using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Zetatech.Accelerate.Data;

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
public abstract class BaseRepository<TEntity, TOptions, TContext> : BaseDisposable, IRepository<TEntity> where TEntity : BaseEntity
                                                                                                         where TOptions : BaseRepositoryOptions
                                                                                                         where TContext : BaseRepositoryContext<TEntity, TOptions>
{
    private TContext _context;
    private Boolean _disposed;
    private DbSet<TEntity> _entities;
    private ILogger _logger;
    private ILoggerFactory _loggerFactory;
    private TOptions _options;
    private SemaphoreSlim _semaphore;

    /// <summary>
    /// Initializes a new instance of the class with the specified options.
    /// </summary>
    /// <param name="options">
    /// The repository options to be used.
    /// </param>
    protected BaseRepository(IOptions<TOptions> options)
    {
        _options = options?.Value;
        _context = (TContext)Activator.CreateInstance(typeof(TContext), this, _options);
        _entities = _context.Set<TEntity>();
        _semaphore = new SemaphoreSlim(1, 1);
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
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    public ILoggerFactory LoggerFactory
    {
        get => _loggerFactory;
        set
        {
            _loggerFactory = value;
            _logger = _loggerFactory?.CreateLogger(GetType().Name);
        }
    }
    /// <summary>
    /// Gets the configuration options of this repository.
    /// </summary>
    protected TOptions Options => _options;

    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
    /// <returns>
    /// The number of affected records.
    /// </returns>
    public virtual Int32 Commit()
    {
        _semaphore.Wait();
        _logger?.LogDebug("Committing pending changes");

        try
        {
            if (!Options.TrackChanges)
            {
                _logger?.LogDebug("Detecting changes before committing pending changes");
                _context.ChangeTracker.DetectChanges();
            }

            return _context.SaveChanges(true);
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
        }
    }
    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    public virtual void Delete(TEntity entity)
    {
        _logger?.LogDebug("Deleting an entity");

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
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting an entity", ex);
        }
    }
    /// <summary>
    /// Deletes the specified collection of entities from the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to delete.
    /// </param>
    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        _logger?.LogDebug("Deleting a collection of entities");

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
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities", ex);
        }
    }
    /// <summary>
    /// Deletes entities that match the specified expression from the data store.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities to delete.
    /// </param>
    public virtual void Delete(Expression<Func<TEntity, Boolean>> expression)
    {
        _logger?.LogDebug("Deleting a collection of entities using a expression");

        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            _logger?.LogDebug("Selecting the collection of entities to delete");

            var entities = _entities.Where(expression);

            foreach (var entity in entities)
            {
                if (_entities.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
            }

            _entities.RemoveRange(entities);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while deleting a collection of entities using a expression", ex);
        }
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected override void Dispose(Boolean disposing)
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
            _logger = null;
            _options = null;
        }
    }
    /// <summary>
    /// Returns the first entity in the data store.
    /// </summary>
    /// <returns>
    /// The first entity.
    /// </returns>
    public virtual TEntity First()
    {
        _logger?.LogDebug("Selecting the first entity in the data store");

        try
        {
            return _entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store", ex);
        }
    }
    /// <summary>
    /// Returns the first entity that matches the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <returns>
    /// The first matching entity.
    /// </returns>
    public virtual TEntity First(Expression<Func<TEntity, Boolean>> expression)
    {
        _logger?.LogDebug("Selecting the first entity in the data store using a expression");

        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            return _entities.FirstOrDefault(expression);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression", ex);
        }
    }
    /// <summary>
    /// Returns the first entity that matches the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    /// <returns>
    /// The first matching entity after skipping the specified number.
    /// </returns>
    public virtual TEntity First(Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        _logger?.LogDebug("Selecting the first entity in the data store using a expression after skipping some of them");

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
            var entities = _entities.Where(expression)
                                    .Skip(skip);

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting the first entity in the data store using a expression after skipping some of them", ex);
        }
    }
    /// <summary>
    /// Inserts the specified entity into the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to insert.
    /// </param>
    public virtual void Insert(TEntity entity)
    {
        _logger?.LogDebug("Inserting an entity into the data store");

        if (entity == null)
        {
            throw new ArgumentException("The entity to insert cannot be null", nameof(entity));
        }

        try
        {
            _entities.Add(entity);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the entity into the data store", ex);
        }
    }
    /// <summary>
    /// Inserts the specified collection of entities into the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to insert.
    /// </param>
    public virtual void Insert(IEnumerable<TEntity> entities)
    {
        _logger?.LogDebug("Insert a collection of entities into to data store");

        if (entities == null)
        {
            throw new ArgumentException("The collection of entities to insert cannot be null", nameof(entities));
        }

        try
        {
            _entities.AddRange(entities);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the collection of entities into to data store", ex);
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
    public virtual void Rollback()
    {
        _semaphore.Wait();
        _logger?.LogDebug("Undoing pending changes");

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

                _context.SaveChanges(true);
            }
            else
            {
                _context.ChangeTracker.DetectChanges();
                _context.ChangeTracker.Clear();
                _entities = _context.Set<TEntity>();
            }
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
        }
    }
    /// <summary>
    /// Returns all entities from the data store.
    /// </summary>
    /// <returns>
    /// A list of all entities.
    /// </returns>
    public virtual IQueryable<TEntity> Select()
    {
        _logger?.LogDebug("Selecting all entities from the data store");

        try
        {
            return _entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting all entities from the data store", ex);
        }
    }
    /// <summary>
    /// Returns all entities that match the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <returns>
    /// A list of matching entities.
    /// </returns>
    public virtual IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression)
    {
        _logger?.LogDebug("Selecting a collection of entities from the data store using a expression");

        if (expression == null)
        {
            throw new ArgumentException("The expression to execute cannot be null", nameof(expression));
        }

        try
        {
            var entities = _entities.Where(expression);

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
        }
    }
    /// <summary>
    /// Returns entities that match the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    /// <returns>
    /// A list of matching entities after skipping the specified number.
    /// </returns>
    public virtual IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip)
    {
        _logger?.LogDebug("Selecting a collection of entities from the data store using a expression after skipping some of them");

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
            var entities = _entities.Where(expression)
                                    .Skip<TEntity>(skip);

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities in the data store using a expression after skipping some of them", ex);
        }
    }
    /// <summary>
    /// Returns entities that match the specified expression, skipping and taking a given number of entities.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    /// <param name="take">
    /// The number of entities to take after skipping.
    /// </param>
    /// <returns>
    /// A list of matching entities after skipping and taking the specified numbers.
    /// </returns>
    public virtual IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take)
    {
        _logger?.LogDebug("Selecting a collection of entities from the data store using a expression after skipping some of them");

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
            var entities = _entities.Where(expression)
                                    .Skip<TEntity>(skip)
                                    .Take<TEntity>(take);

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression after skipping some of them", ex);
        }
    }
    /// <summary>
    /// Execute a custom query string to select entities.
    /// </summary>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    /// <returns>
    /// The list of entities selected.
    /// </returns>
    public abstract IQueryable<TEntity> Select(String queryString, params IDbDataParameter[] parameters);
    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to update.
    /// </param>
    public virtual void Update(TEntity entity)
    {
        _logger?.LogDebug("Updating an entity");

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

            _entities.Update(entity);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating an entity", ex);
        }
    }
    /// <summary>
    /// Updates the specified collection of entities in the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to update.
    /// </param>
    public virtual void Update(IEnumerable<TEntity> entities)
    {
        _logger?.LogDebug("Updating a collection of entities");

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

            _entities.UpdateRange(entities);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while updating a collection of entities", ex);
        }
    }
}