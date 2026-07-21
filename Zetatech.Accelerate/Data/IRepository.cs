using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zetatech.Accelerate.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity, new()
{
    void Delete(TEntity entity);
    void Delete(IList<TEntity> entities);
    void Delete(Expression<Func<TEntity, Boolean>> expression);
    void Insert(TEntity entity);
    void Insert(IList<TEntity> entities);
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression = null,
                               Int32? skip = null,
                               Int32? take = null);
    TEntity Single(Expression<Func<TEntity, Boolean>> expression = null,
                   Int32? skip = null);
    void Update(TEntity entity);
    void Update(IList<TEntity> entities);
}
