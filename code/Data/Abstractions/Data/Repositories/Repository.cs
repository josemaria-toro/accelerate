using Accelerate.Data.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Base class for data repositories.
/// </summary>
/// <typeparam name="TEntity">
/// Type of data entity managed in the repository.
/// </typeparam>
/// <typeparam name="TOptions">
/// Type of configuration options.
/// </typeparam>
public abstract class Repository<TEntity, TOptions> : IRepository<TEntity> where TEntity : class, IEntity, new()
                                                                           where TOptions : RepositoryOptions
{
    private Boolean _disposed;
    private TOptions _options;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for data repository.
    /// </param>
    protected Repository(IOptions<TOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("Options for data repository cannot be null", nameof(options));
    }

    /// <summary>
    /// Configuration options of repository.
    /// </summary>
    protected internal TOptions Options => _options;

    /// <summary>
    /// Persist pending repository changes.
    /// </summary>
    public abstract Int32 Commit();
    /// <summary>
    /// Remove a data entity.
    /// </summary>
    /// <param name="entity">
    /// Data entity information.
    /// </param>
    public abstract void Delete(TEntity entity);
    /// <summary>
    /// Remove the list of data entities.
    /// </summary>
    /// <param name="entities">
    /// Data entities information.
    /// </param>
    public abstract void Delete(IEnumerable<TEntity> entities);
    /// <summary>
    /// Remove the list of data entities retrieved by the expression.
    /// </summary>
    /// <param name="expression">
    /// Expression to determine the data entities to delete.
    /// </param>
    public abstract void Delete(Expression<Func<TEntity, Boolean>> expression);
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
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        if (disposing)
        {
            _options = null;
        }

        _disposed = true;
    }
    /// <summary>
    /// Insert a data entity.
    /// </summary>
    /// <param name="entity">
    /// Data entity information.
    /// </param>
    public abstract void Insert(TEntity entity);
    /// <summary>
    /// Insert a list of data entities.
    /// </summary>
    /// <param name="entities">
    /// Data entities information.
    /// </param>
    public abstract void Insert(IEnumerable<TEntity> entities);
    /// <summary>
    /// Undo pending repository changes.
    /// </summary>
    public abstract void Rollback();
    /// <summary>
    /// Retrieve all data entities in repository.
    /// </summary>
    public abstract IEnumerable<TEntity> Select();
    /// <summary>
    /// Retrieve a list of data entities by expression.
    /// </summary>
    /// <param name="expression">
    /// Expression to determine the data entities to retrieve.
    /// </param>
    public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Retrieve a list of data entities by expression.
    /// </summary>
    /// <param name="expression">
    /// Expression to determine the data entities to retrieve.
    /// </param>
    /// <param name="skip">
    /// Number of data entities to skip.
    /// </param>
    public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    /// <summary>
    /// Select a list of data entities by expression.
    /// </summary>
    /// <param name="expression">
    /// Expression to determine the data entities to select.
    /// </param>
    /// <param name="skip">
    /// Number of data entities to skip.
    /// </param>
    /// <param name="take">
    /// Number of data entities to take.
    /// </param>
    public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
    /// <summary>
    /// Update a data entity.
    /// </summary>
    /// <param name="entity">
    /// Data entity information.
    /// </param>
    public abstract void Update(TEntity entity);
    /// <summary>
    /// Update a list of data entities.
    /// </summary>
    /// <param name="entities">
    /// Data entities information.
    /// </param>
    public abstract void Update(IEnumerable<TEntity> entities);
}