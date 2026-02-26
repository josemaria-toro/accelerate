using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Zetatech.Accelerate.Domain;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);
    void Delete(Expression<Func<TEntity, Boolean>> expression);
    void Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> Select();
    IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
    IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
    TEntity Single();
    TEntity Single(Expression<Func<TEntity, Boolean>> expression);
    TEntity Single(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
}
