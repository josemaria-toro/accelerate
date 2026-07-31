using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public static class IRepositoryAsync
{
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Delete(entity), cancellationToken);
    }
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Delete(entities), cancellationToken);
    }
    public static async Task DeleteAsync<TEntity>(this IRepository<TEntity> repository,
                                                  Expression<Func<TEntity, Boolean>> expression,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Delete(expression), cancellationToken);
    }
    public static async Task InsertAsync<TEntity>(this IRepository<TEntity> repository,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Insert(entity), cancellationToken);
    }
    public static async Task InsertAsync<TEntity>(this IRepository<TEntity> repository,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Insert(entities), cancellationToken);
    }
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IRepository<TEntity> repository,
                                                                       Expression<Func<TEntity, Boolean>> expression = null,
                                                                       Int32? skip = null,
                                                                       Int32? take = null,
                                                                       CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        return await Task.Run(() => repository.Select(expression, skip, take), cancellationToken);
    }
    public static async Task<TEntity> SingleAsync<TEntity>(this IRepository<TEntity> repository,
                                                           Expression<Func<TEntity, Boolean>> expression = null,
                                                           Int32? skip = null,
                                                           CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        return await Task.Run(() => repository.Single(expression, skip), cancellationToken);
    }
    public static async Task UpdateAsync<TEntity>(this IRepository<TEntity> repository,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Update(entity), cancellationToken);
    }
    public static async Task UpdateAsync<TEntity>(this IRepository<TEntity> repository,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => repository.Update(entities), cancellationToken);
    }
}
