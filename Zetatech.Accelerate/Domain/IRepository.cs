using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Domain;

/// <summary>
/// Provides the interface for implementing custom domain repositories.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity managed by the repository.
/// </typeparam>
public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    /// <summary>
    /// Apply the pending changes in the table schema.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    Task ApplyChangesInTableSchemaAsync(Guid operationId);
    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    Task<Int32> CommitAsync(Guid operationId);
    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    Task DeleteAsync(Guid operationId, TEntity entity);
    /// <summary>
    /// Deletes the specified collection of entities from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to delete.
    /// </param>
    Task DeleteAsync(Guid operationId, IEnumerable<TEntity> entities);
    /// <summary>
    /// Deletes entities that match the specified expression from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities to delete.
    /// </param>
    Task DeleteAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression);
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
    Task<IEnumerable<TEntity>> ExecuteAsync(Guid operationId, String queryString, params IDbDataParameter[] parameters);
    /// <summary>
    /// Inserts the specified entity into the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to insert.
    /// </param>
    Task InsertAsync(Guid operationId, TEntity entity);
    /// <summary>
    /// Inserts the specified collection of entities into the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to insert.
    /// </param>
    Task InsertAsync(Guid operationId, IEnumerable<TEntity> entities);
    /// <summary>
    /// Rolls back all pending changes that have not been committed.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    Task RollbackAsync(Guid operationId);
    /// <summary>
    /// Retrieve all entities from the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    Task<IEnumerable<TEntity>> SelectAsync(Guid operationId);
    /// <summary>
    /// Retrieve all entities that match the specified expression.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping a given number of entities.
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
    Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping and taking a given number of entities.
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
    Task<IEnumerable<TEntity>> SelectAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
    /// <summary>
    /// Retrieves the first entity in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    Task<TEntity> SingleAsync(Guid operationId);
    /// <summary>
    /// Retrieves the first entity that matches the specified expression.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    Task<TEntity> SingleAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Retrieves the first entity that matches the specified expression, skipping a given number of entities.
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
    Task<TEntity> SingleAsync(Guid operationId, Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entity">
    /// The entity to update.
    /// </param>
    Task UpdateAsync(Guid operationId, TEntity entity);
    /// <summary>
    /// Updates the specified collection of entities in the data store.
    /// </summary>
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="entities">
    /// The entities to update.
    /// </param>
    Task UpdateAsync(Guid operationId, IEnumerable<TEntity> entities);
}
