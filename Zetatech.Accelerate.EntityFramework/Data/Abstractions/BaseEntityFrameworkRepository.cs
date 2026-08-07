using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Contexts;

namespace Zetatech.Accelerate.Data.Abstractions;

public abstract class BaseEntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
{
    private Boolean _disposed;
    private EntityFrameworkContext<TEntity> _context;
    private DbSet<TEntity> _entities;
    private SemaphoreSlim _semaphore;

    protected BaseEntityFrameworkRepository(IOptions<EntityFrameworkRepositoryOptions> options)
    {
        _context = new EntityFrameworkContext<TEntity>(options);
        _entities = _context.Set<TEntity>();
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public void Delete(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to delete must be a valid instance", nameof(entity));
        }

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);

            SavePendingChanges(true);
        }
        catch (DataException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException($"Unexpected error was thrown while deleting the entity with id '{entity.Id}'", ex);
        }
    }
    public void Delete(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided list of entities to delete must be a valid instance", nameof(entities));
        }

        try
        {
            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }

                _entities.Remove(entity);
            }

            SavePendingChanges(true);
        }
        catch (DataException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while deleting a list of entities", ex);
        }
    }
    public void Delete(Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The provided expression to determine the set of entities to delete must be a valid instance", nameof(expression));
        }

        try
        {
            var entities = _entities.Where(expression)
                                    .ToList();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (_entities.Entry(entity).State == EntityState.Detached)
                    {
                        _entities.Attach(entity);
                    }

                    _entities.Remove(entity);
                }

                SavePendingChanges(true);
            }
        }
        catch (DataException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while deleting a list of entities after filtering the data source using a expression", ex);
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        if (disposing)
        {
            _context = null;
            _entities = null;
            _semaphore = null;
        }
    }
    public void Insert(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to insert must be a valid instance", nameof(entity));
        }

        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        _entities.Add(entity);

        SavePendingChanges(true);
    }
    public void Insert(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided collection of entities to insert must be a valid instance", nameof(entities));
        }

        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _entities.Add(entity);
        }

        SavePendingChanges(true);
    }

    private void RollbackPendingChanges()
    {
        try
        {
            var entityEntries = _context.ChangeTracker.Entries()
                                                      .Where(x => x.State != EntityState.Unchanged);

            foreach (var entityEntry in entityEntries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                }
            }

            _context.SaveChanges(true);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("Something was wrong while undoing pending changes", ex);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while undoing pending changes", ex);
        }
    }
    private void SavePendingChanges(Boolean performRollbackOnFailure)
    {
        _semaphore.Wait();

        try
        {
            _context.SaveChanges(true);
        }
        catch (Exception cex)
        {
            if (performRollbackOnFailure)
            {
                try
                {
                    RollbackPendingChanges();
                }
                catch (DataException rex)
                {
                    throw new AggregateException("Unexpected error was thrown while saving pending changes", cex, rex);
                }
            }

            throw new DataException("Unexpected error was thrown while saving pending changes", cex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression = null,
                                      Int32? skip = null,
                                      Int32? take = null)
    {
        try
        {
            var entities = _entities.AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            return entities.Skip<TEntity>(skip ?? 0)
                           .Take<TEntity>(take ?? 100);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while selecting entities from the data source", ex);
        }
    }
    public TEntity Single(Expression<Func<TEntity, Boolean>> expression = null,
                          Int32? skip = null)
    {
        try
        {
            var entities = _entities.AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            return entities.Skip<TEntity>(skip ?? 0)
                           .FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while selecting the first entity in the data source", ex);
        }
    }
    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to update must be a valid instance", nameof(entity));
        }

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Update(entity);

            SavePendingChanges(true);
        }
        catch (DataException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException($"Unexpected error was thrown while updating the entity with id {entity.Id}", ex);
        }
    }
    public void Update(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided list of entities to update must be a valid instance", nameof(entities));
        }

        try
        {
            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }

                entity.UpdatedAt = DateTime.UtcNow;

                _entities.Update(entity);
            }

            SavePendingChanges(true);
        }
        catch (DataException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while updating the list of entities", ex);
        }
    }
}