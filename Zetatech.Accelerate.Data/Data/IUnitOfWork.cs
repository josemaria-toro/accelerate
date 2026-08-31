using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task DeleteAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task DeleteAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task DeleteAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task InsertAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task InsertAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> SelectAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                                   Int32? skip = null,
                                                   Int32? take = null,
                                                   CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                       Int32? skip = null,
                                       CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task UpdateAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    Task UpdateAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
}
