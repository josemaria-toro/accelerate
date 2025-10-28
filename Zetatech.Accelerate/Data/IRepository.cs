using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Zetatech.Accelerate.Data;

/// <summary>
/// Provides the interface for implementing custom data access repositories.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity managed by the repository.
/// </typeparam>
public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    ILoggerFactory LoggerFactory { get; set; }

    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
    /// <returns>
    /// The number of affected records.
    /// </returns>
    Int32 Commit();
    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    void Delete(TEntity entity);
    /// <summary>
    /// Deletes the specified collection of entities from the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to delete.
    /// </param>
    void Delete(IEnumerable<TEntity> entities);
    /// <summary>
    /// Deletes entities that match the specified expression from the data store.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities to delete.
    /// </param>
    void Delete(Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Returns the first entity in the data store.
    /// </summary>
    /// <returns>
    /// The first entity.
    /// </returns>
    TEntity First();
    /// <summary>
    /// Returns the first entity that matches the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <returns>
    /// The first matching entity.
    /// </returns>
    TEntity First(Expression<Func<TEntity, Boolean>> expression);
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
    TEntity First(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    /// <summary>
    /// Inserts the specified entity into the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to insert.
    /// </param>
    void Insert(TEntity entity);
    /// <summary>
    /// Inserts the specified collection of entities into the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to insert.
    /// </param>
    void Insert(IEnumerable<TEntity> entities);
    /// <summary>
    /// Rolls back all pending changes that have not been committed.
    /// </summary>
    void Rollback();
    /// <summary>
    /// Returns all entities from the data store.
    /// </summary>
    /// <returns>
    /// A list of all entities.
    /// </returns>
    IQueryable<TEntity> Select();
    /// <summary>
    /// Returns all entities that match the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <returns>
    /// A list of matching entities.
    /// </returns>
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
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
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
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
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
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
    IQueryable<TEntity> Select(String queryString, params IDbDataParameter[] parameters);
    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="entity">
    /// The entity to update.
    /// </param>
    void Update(TEntity entity);
    /// <summary>
    /// Updates the specified collection of entities in the data store.
    /// </summary>
    /// <param name="entities">
    /// The entities to update.
    /// </param>
    void Update(IEnumerable<TEntity> entities);
}
