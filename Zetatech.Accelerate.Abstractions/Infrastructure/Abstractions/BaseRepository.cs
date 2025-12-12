using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Domain;
using Zetatech.Accelerate.Domain.Abstractions;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base class for implementing custom repositories.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity managed by the repository. Must inherit from <see cref="BaseEntity"/>.
/// </typeparam>
/// <typeparam name="TOptions">
/// The type of repository options. Must inherit from <see cref="BaseRepositoryOptions"/>.
/// </typeparam>
/// <typeparam name="TContext">
/// The type of context used for this repository. Must inherit from <see cref="BaseRepositoryContext{TEntity, TOptions}"/>.
/// </typeparam>
public abstract class BaseRepository<TEntity, TOptions, TContext> : IRepository<TEntity> where TEntity : BaseEntity
                                                                                         where TOptions : BaseRepositoryOptions
                                                                                         where TContext : BaseRepositoryContext<TEntity, TOptions>
{
    private TContext _context;
    private Boolean _disposed;
    private DbSet<TEntity> _entities;
    private TOptions _options;
    private SemaphoreSlim _semaphore;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initializes a new instance of the class with the specified options.
    /// </summary>
    /// <param name="options">
    /// The repository options to be used.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseRepository(IOptions<TOptions> options, ITrackingService trackingService = null)
    {
        _options = options?.Value;
        _context = (TContext)Activator.CreateInstance(typeof(TContext), this, _options);
        _entities = _context.Set<TEntity>();
        _semaphore = new SemaphoreSlim(1, 1);
        _trackingService = trackingService;
    }

    /// <summary>
    /// Gets the entity framework context.
    /// </summary>
    protected TContext Context => _context;
    /// <summary>
    /// Gets the set of entities managed by this repository.
    /// </summary>
    protected DbSet<TEntity> Entities => _entities;
    /// <summary>
    /// Gets the configuration options of this repository.
    /// </summary>
    protected TOptions Options => _options;
    /// <summary>
    /// Gets the service for tracking application data.
    /// </summary>
    protected ITrackingService TrackingService => _trackingService;

    /// <summary>
    /// Apply the pending changes in the table schema.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public abstract Task ApplyChangesInTableSchemaAsync(Guid operationId);
    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public virtual async Task<Int32> CommitAsync(Guid operationId)
    {
        await _semaphore.WaitAsync();

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Commit",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            if (!Options.TrackChanges)
            {
                _context.ChangeTracker.DetectChanges();
            }

            var affectedRows = await _context.SaveChangesAsync(true);
            dependency.OutputData = $"{affectedRows}";
            dependency.Success = true;

            return affectedRows;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DataException("A concurrency violation is encountered while committing pending changes", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("An error is encountered while committing pending changes", ex);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while commiting pending changes", ex);
        }
        finally
        {
            _semaphore.Release();
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    public virtual async Task DeleteAsync(Guid operationId, TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to delete cannot be null", nameof(entity));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{entity.Id}",
            Name = "Delete",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting an entity", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Deletes the specified collection of entities from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to delete.
    /// </param>
    public virtual async Task DeleteAsync(Guid operationId, IEnumerable<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to delete cannot be null", nameof(entities));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{String.Join(',', entities.Select(x => x.Id))}",
            Name = "Delete",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

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
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Deletes entities that match the specified expression from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities to delete.
    /// </param>
    public virtual async Task DeleteAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Delete",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression);
            dependency.InputData = queryExpression.ToQueryString();
            var entities = await queryExpression.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";

            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            _entities.RemoveRange(entities);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities using a expression", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
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
            _options = null;
        }
    }
    /// <summary>
    /// Execute a custom query string to select entities.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    public abstract Task<IEnumerable<TEntity>> ExecuteAsync(Guid operationId, String queryString, params IDbDataParameter[] parameters);
    /// <summary>
    /// Inserts the specified entity into the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to insert.
    /// </param>
    public virtual async Task InsertAsync(Guid operationId, TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to insert cannot be null", nameof(entity));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{entity.Id}",
            Name = "Insert",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            await _entities.AddAsync(entity);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the entity into the data store", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Inserts the specified collection of entities into the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to insert.
    /// </param>
    public virtual async Task InsertAsync(Guid operationId, IEnumerable<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to insert cannot be null", nameof(entities));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{String.Join(',', entities.Select(x => x.Id))}",
            Name = "Insert",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            await _entities.AddRangeAsync(entities);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the collection of entities into to data store", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">
    /// The builder being used to construct the model for this context.
    /// </param>
    protected virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TEntity>();
    }
    /// <summary>
    /// Rolls back all pending changes that have not been committed.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public virtual async Task RollbackAsync(Guid operationId)
    {
        await _semaphore.WaitAsync();

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Rollback",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            if (_options.TrackChanges)
            {
                var entityEntries = _context.ChangeTracker.Entries()
                                                          .Where(entityEntry => entityEntry.State != EntityState.Unchanged);

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

                await _context.SaveChangesAsync(true);
            }
            else
            {
                _context.ChangeTracker.DetectChanges();
                _context.ChangeTracker.Clear();
                _entities = _context.Set<TEntity>();
            }

            dependency.Success = true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DataException("A concurrency violation is encountered while undoing changes", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("An error is encountered while undoing changes", ex);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while undoing changes", ex);
        }
        finally
        {
            _semaphore.Release();
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns all entities from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync(Guid operationId)
    {
        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = _entities.ToQueryString(),
            Name = "Select",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var entities = await _entities.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";
            dependency.Success = true;

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting all entities from the data store", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns all entities that match the specified expression.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Select",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression);
            dependency.InputData = queryExpression.ToQueryString();
            var entities = await queryExpression.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";
            dependency.Success = true;

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns entities that match the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        if (skip < 0)
        {
            throw new ArgumentException("The number of entities to skip must be upper or equals than 0", nameof(skip));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Select",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression)
                                           .Skip<TEntity>(skip);
            dependency.InputData = queryExpression.ToQueryString();
            var entities = await queryExpression.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";
            dependency.Success = true;

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities in the data store using a expression after skipping some of them", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns entities that match the specified expression, skipping and taking a given number of entities.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    /// <param name="take">
    /// The number of entities to take after skipping.
    /// </param>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take)
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

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Select",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression)
                                           .Skip<TEntity>(skip)
                                           .Take<TEntity>(take);
            dependency.InputData = queryExpression.ToQueryString();
            var entities = await queryExpression.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";
            dependency.Success = true;

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression after skipping some of them", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns the first entity in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public virtual async Task<TEntity> SingleAsync(Guid operationId)
    {
        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Single",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var entity = await _entities.FirstOrDefaultAsync();
            dependency.OutputData = $"{entity?.Id}";
            dependency.Success = true;

            return entity;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns the first entity that matches the specified expression.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    public virtual async Task<TEntity> SingleAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Single",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression);
            dependency.InputData = queryExpression.ToQueryString();
            var entity = await queryExpression.FirstOrDefaultAsync();
            dependency.OutputData = $"{entity?.Id}";
            dependency.Success = true;

            return entity;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Returns the first entity that matches the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    public virtual async Task<TEntity> SingleAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        if (skip < 0)
        {
            throw new ArgumentException("The number of entities to skip must be upper or equals than 0", nameof(skip));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Single",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = _entities.Where(expression)
                                           .Skip(skip);
            dependency.InputData = queryExpression.ToQueryString();
            var entity = await queryExpression.FirstOrDefaultAsync();
            dependency.OutputData = $"{entity?.Id}";
            dependency.Success = true;

            return entity;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression after skipping some of them", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to update.
    /// </param>
    public virtual async Task UpdateAsync(Guid operationId, TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The entity to update cannot be null", nameof(entity));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{entity.Id}",
            Name = "Update",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Update(entity);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating an entity", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
    /// <summary>
    /// Updates the specified collection of entities in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to update.
    /// </param>
    public virtual async Task UpdateAsync(Guid operationId, IEnumerable<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to update cannot be null", nameof(entities));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            InputData = $"{String.Join(',', entities.Select(x => x.Id))}",
            Name = "Update",
            OperationId = operationId,
            TargetName = _context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            _entities.UpdateRange(entities);
            dependency.Success = true;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating a collection of entities", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
}