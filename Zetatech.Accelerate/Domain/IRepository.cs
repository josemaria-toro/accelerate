using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zetatech.Accelerate.Domain;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    void Delete(TEntity entity);
    void Delete(IList<TEntity> entities);
    void Delete(Expression<Func<TEntity, Boolean>> expression);
    void Insert(TEntity entity);
    void Insert(IList<TEntity> entities);
    IQueryable<TEntity> Select();
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    IQueryable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
    TEntity Single();
    TEntity Single(Expression<Func<TEntity, Boolean>> expression);
    TEntity Single(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    void Update(TEntity entity);
    void Update(IList<TEntity> entities);
}
