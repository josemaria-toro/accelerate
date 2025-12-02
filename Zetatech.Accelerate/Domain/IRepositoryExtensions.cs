using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Domain;

/// <summary>
/// Extensions methods for the <see cref="IRepository{TEntity}"/> interface.
/// </summary>
public static class IRepositoryExtensions
{
    /// <summary>
    /// Commits all pending changes to the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    public static async Task<Int32> CommitAsync<TEntity>(this IRepository<TEntity> repository) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Commit());
    }
    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : IEntity
    {
        await Task.Run(() => repository.Delete(entity));
    }
    /// <summary>
    /// Deletes the specified collection of entities from the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entities">
    /// The entities to delete.
    /// </param>
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities) where TEntity : IEntity
    {
        await Task.Run(() => repository.Delete(entities));
    }
    /// <summary>
    /// Deletes entities that match the specified expression from the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities to delete.
    /// </param>
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression) where TEntity : IEntity
    {
        await Task.Run(() => repository.Delete(expression));
    }
    /// <summary>
    /// Execute a custom query string to select entities.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    public static async Task<IQueryable<TEntity>> ExecuteAsync<TEntity>(this IRepository<TEntity> repository, String queryString, params IDbDataParameter[] parameters) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Execute(queryString, parameters));
    }
    /// <summary>
    /// Inserts the specified entity into the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entity">
    /// The entity to insert.
    /// </param>
    public static async Task InsertAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : IEntity
    {
        await Task.Run(() => repository.Insert(entity));
    }
    /// <summary>
    /// Inserts the specified collection of entities into the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entities">
    /// The entities to insert.
    /// </param>
    public static async Task InsertAsync<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities) where TEntity : IEntity
    {
        await Task.Run(() => repository.Insert(entities));
    }
    /// <summary>
    /// Rolls back all pending changes that have not been committed.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    public static async Task RollbackAsync<TEntity>(this IRepository<TEntity> repository) where TEntity : IEntity
    {
        await Task.Run(() => repository.Rollback());
    }
    /// <summary>
    /// Retrieve all entities from the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IRepository<TEntity> repository) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Select());
    }
    /// <summary>
    /// Retrieve all entities that match the specified expression.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Select(expression));
    }
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression, Int32 skip) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Select(expression, skip));
    }
    /// <summary>
    /// Retrieve entities that match the specified expression, skipping and taking a given number of entities.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
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
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Select(expression, skip, take));
    }
    /// <summary>
    /// Retrieves the first entity in the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    public static async Task<TEntity> SingleAsync<TEntity>(this IRepository<TEntity> repository) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Single());
    }
    /// <summary>
    /// Retrieves the first entity that matches the specified expression.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    public static async Task<TEntity> SingleAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Single(expression));
    }
    /// <summary>
    /// Retrieves the first entity that matches the specified expression, skipping a given number of entities.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="expression">
    /// The expression to filter entities.
    /// </param>
    /// <param name="skip">
    /// The number of entities to skip.
    /// </param>
    public static async Task<TEntity> SingleAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, Boolean>> expression, Int32 skip) where TEntity : IEntity
    {
        return await Task.FromResult(repository.Single(expression, skip));
    }
    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entity">
    /// The entity to update.
    /// </param>
    public static async Task UpdateAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : IEntity
    {
        await Task.Run(() => repository.Update(entity));
    }
    /// <summary>
    /// Updates the specified collection of entities in the data store.
    /// </summary>
    /// <param name="repository">
    /// The instance of the data access repository.
    /// </param>
    /// <param name="entities">
    /// The entities to update.
    /// </param>
    public static async Task UpdateAsync<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities) where TEntity : IEntity
    {
        await Task.Run(() => repository.Update(entities));
    }
}
