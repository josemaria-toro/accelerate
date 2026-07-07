using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    Task CommitAsync(CancellationToken cancellationToken = default);
    void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    Task DeleteAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Delete<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
    Task DeleteAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Delete<TEntity>(Expression<Func<TEntity, Boolean>> expression) where TEntity : class, IEntity, new();
    Task DeleteAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    Task InsertAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Insert<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
    Task InsertAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IQueryable<TEntity> Select<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                        Int32? skip = null,
                                        Int32? take = null) where TEntity : class, IEntity, new();
    Task<IQueryable<TEntity>> SelectAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                                   Int32? skip = null,
                                                   Int32? take = null,
                                                   CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    TEntity Single<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                            Int32? skip = null) where TEntity : class, IEntity, new();
    Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                       Int32? skip = null,
                                       CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Update<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    Task UpdateAsync<TEntity>(TEntity entity,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
    void Update<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
    Task UpdateAsync<TEntity>(IList<TEntity> entities,
                              CancellationToken cancellationToken = default) where TEntity : class, IEntity, new();
}
