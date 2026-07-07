using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity, new()
{
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity,
                     CancellationToken cancellationToken = default);
    void Delete(IList<TEntity> entities);
    Task DeleteAsync(IList<TEntity> entities,
                     CancellationToken cancellationToken = default);
    void Delete(Expression<Func<TEntity, Boolean>> expression);
    Task DeleteAsync(Expression<Func<TEntity, Boolean>> expression,
                     CancellationToken cancellationToken = default);
    void Insert(TEntity entity);
    Task InsertAsync(TEntity entity,
                     CancellationToken cancellationToken = default);
    void Insert(IList<TEntity> entities);
    Task InsertAsync(IList<TEntity> entities,
                     CancellationToken cancellationToken = default);
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression = null,
                               Int32? skip = null,
                               Int32? take = null);
    Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, Boolean>> expression = null,
                                          Int32? skip = null,
                                          Int32? take = null,
                                          CancellationToken cancellationToken = default);
    TEntity Single(Expression<Func<TEntity, Boolean>> expression = null,
                   Int32? skip = null);
    Task<TEntity> SingleAsync(Expression<Func<TEntity, Boolean>> expression = null,
                              Int32? skip = null,
                              CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity,
                     CancellationToken cancellationToken = default);
    void Update(IList<TEntity> entities);
    Task UpdateAsync(IList<TEntity> entities,
                     CancellationToken cancellationToken = default);
}
