using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity, new()
{
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(Expression<Func<TEntity, Boolean>> expression, CancellationToken cancellationToken = default);
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, Boolean>> expression = null, Int32? skip = null, Int32? take = null, CancellationToken cancellationToken = default);
    Task<TEntity> SingleAsync(Expression<Func<TEntity, Boolean>> expression = null, Int32? skip = null, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);
}
