using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public static class IUnitOfWorkAsync
{
    public static async Task DeleteAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Delete(entity), cancellationToken);
    }
    public static async Task DeleteAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Delete(entities), cancellationToken);
    }
    public static async Task DeleteAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  Expression<Func<TEntity, Boolean>> expression,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Delete(expression), cancellationToken);
    }
    public static async Task InsertAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Insert(entity), cancellationToken);
    }
    public static async Task InsertAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Insert(entities), cancellationToken);
    }
    public static async Task<IQueryable<TEntity>> SelectAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                                       Expression<Func<TEntity, Boolean>> expression = null,
                                                                       Int32? skip = null,
                                                                       Int32? take = null,
                                                                       CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        return await Task.Run(() => unitOfWork.Select(expression, skip, take), cancellationToken);
    }
    public static async Task<TEntity> SingleAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                           Expression<Func<TEntity, Boolean>> expression = null,
                                                           Int32? skip = null,
                                                           CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        return await Task.Run(() => unitOfWork.Single(expression, skip), cancellationToken);
    }
    public static async Task UpdateAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  TEntity entity,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Update(entity), cancellationToken);
    }
    public static async Task UpdateAsync<TEntity>(this IUnitOfWork unitOfWork,
                                                  IList<TEntity> entities,
                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        await Task.Run(() => unitOfWork.Update(entities), cancellationToken);
    }
}
