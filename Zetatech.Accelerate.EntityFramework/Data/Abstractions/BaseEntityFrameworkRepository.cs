using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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

    public async Task DeleteAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
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

            await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
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
    public async Task DeleteAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
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

            await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
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
    public async Task DeleteAsync(Expression<Func<TEntity, Boolean>> expression,
                                  CancellationToken cancellationToken = default)
    {
        if (expression == null)
        {
            throw new ArgumentException("The provided expression to determine the set of entities to delete must be a valid instance", nameof(expression));
        }

        try
        {
            var entities = await _entities.Where(expression)
                                          .ToListAsync(cancellationToken)
                                          .ConfigureAwait(false);

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

                await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
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
    public async Task InsertAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
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

        await _entities.AddAsync(entity, cancellationToken)
                       .ConfigureAwait(false);
        await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
    }
    public async Task InsertAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
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

            await _entities.AddAsync(entity, cancellationToken)
                           .ConfigureAwait(false);
        }

        await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
    }

    private async Task RollbackPendingChangesAsync(CancellationToken cancellationToken = default)
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

            await _context.SaveChangesAsync(true, cancellationToken)
                          .ConfigureAwait(false);
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
    private async Task SavePendingChangesAsync(Boolean performRollbackOnFailure,
                                               CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken)
                        .ConfigureAwait(false);

        try
        {
            await _context.SaveChangesAsync(true, cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (Exception cex)
        {
            if (performRollbackOnFailure)
            {
                try
                {
                    await RollbackPendingChangesAsync(cancellationToken).ConfigureAwait(false);
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
    public async Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, Boolean>> expression = null,
                                                       Int32? skip = null,
                                                       Int32? take = null,
                                                       CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var entities = _entities.AsNoTracking()
                                    .AsQueryable();

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
    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, Boolean>> expression = null,
                                           Int32? skip = null,
                                           CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = _entities.AsNoTracking()
                                    .AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            return await entities.Skip<TEntity>(skip ?? 0)
                                 .FirstOrDefaultAsync(cancellationToken)
                                 .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error was thrown while selecting the first entity in the data source", ex);
        }
    }
    public async Task UpdateAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
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

            await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
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
    public async Task UpdateAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
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

            await SavePendingChangesAsync(true, cancellationToken).ConfigureAwait(false);
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