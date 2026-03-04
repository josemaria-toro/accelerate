using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Domain;
using Zetatech.Accelerate.Domain.Abstractions;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private RepositoryContext<TEntity> _context;
    private Boolean _disposed;
    private DbSet<TEntity> _entities;
    private RepositoryOptions _options;
    private SemaphoreSlim _semaphore;

    public Repository(IOptions<RepositoryOptions> options)
    {
        _options = options?.Value;
        _context = new RepositoryContext<TEntity>(_options);
        _entities = _context.Set<TEntity>();
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public void Delete(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to delete cannot be null", nameof(entity));
        }

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting an entity", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public void Delete(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to delete cannot be null", nameof(entities));
        }

        try
        {
            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            _entities.RemoveRange(entities);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public void Delete(Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            var entities = _entities.Where(expression)
                                    .ToList();

            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            _entities.RemoveRange(entities);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities using a expression", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _context = null;
        _entities = null;
        _options = null;
        _semaphore = null;

        GC.SuppressFinalize(this);
    }
    public void Insert(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to insert cannot be null", nameof(entity));
        }

        try
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _entities.Add(entity);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the entity into the data store", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public void Insert(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to insert cannot be null", nameof(entities));
        }

        try
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _entities.AddRange(entities);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the collection of entities into to data store", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public IQueryable<TEntity> Select()
    {
        try
        {
            return _entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting all entities from the data store", ex);
        }
    }
    public IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            return _entities.Where(expression);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
        }
    }
    public IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        if (skip < 0)
        {
            throw new ArgumentException("The number of entities to skip must be upper or equals than 0", nameof(skip));
        }

        try
        {
            return _entities.Where(expression)
                            .Skip<TEntity>(skip);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities in the data store using a expression after skipping some of them", ex);
        }
    }
    public IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        if (skip < 0)
        {
            throw new ArgumentException("The number of entities to skip must be upper or equals than 0", nameof(skip));
        }

        if (take < 1)
        {
            throw new ArgumentException("The number of entities to take must be upper than 0", nameof(take));
        }

        try
        {
            return _entities.Where(expression)
                            .Skip<TEntity>(skip)
                            .Take<TEntity>(take);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression after skipping some of them", ex);
        }
    }
    public TEntity Single()
    {
        try
        {
            return _entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store", ex);
        }
    }
    public TEntity Single(Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            return _entities.Where(expression)
                            .FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression", ex);
        }
    }
    public TEntity Single(Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        if (skip < 0)
        {
            throw new ArgumentException("The number of entities to skip must be upper or equals than 0", nameof(skip));
        }

        try
        {
            return _entities.Where(expression)
                            .Skip(skip)
                            .FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression after skipping some of them", ex);
        }
    }
    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to update cannot be null", nameof(entity));
        }

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Update(entity);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating an entity", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public void Update(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to update cannot be null", nameof(entities));
        }

        try
        {
            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _entities.UpdateRange(entities);
            _semaphore.Wait();
            _context.Commit();
        }
        catch (DataException)
        {
            _context.Rollback();
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating a collection of entities", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}