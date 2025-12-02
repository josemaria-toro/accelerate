using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

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
    void ApplyChangesInTableSchema();
    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
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
    /// Execute a custom query string to select entities.
    /// </summary>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    IQueryable<TEntity> Execute(String queryString, params IDbDataParameter[] parameters);
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
    /// Retrieve all entities from the data store.
    /// </summary>
    IQueryable<TEntity> Select();
    /// <summary>
    /// Retrieve all entities that match the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping and taking a given number of entities.
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
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
    /// <summary>
    /// Retrieves the first entity in the data store.
    /// </summary>
    TEntity Single();
    /// <summary>
    /// Retrieves the first entity that matches the specified expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    TEntity Single(Expression<Func<TEntity, Boolean>> expression);
    /// <summary>
    /// Retrieves the first entity that matches the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    TEntity Single(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
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
