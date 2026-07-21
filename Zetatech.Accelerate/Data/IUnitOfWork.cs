using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zetatech.Accelerate.Data;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    void Delete<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
    void Delete<TEntity>(Expression<Func<TEntity, Boolean>> expression) where TEntity : class, IEntity, new();
    void Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    void Insert<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
    void Rollback();
    IQueryable<TEntity> Select<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                                        Int32? skip = null,
                                        Int32? take = null) where TEntity : class, IEntity, new();
    TEntity Single<TEntity>(Expression<Func<TEntity, Boolean>> expression = null,
                            Int32? skip = null) where TEntity : class, IEntity, new();
    void Update<TEntity>(TEntity entity) where TEntity : class, IEntity, new();
    void Update<TEntity>(IList<TEntity> entities) where TEntity : class, IEntity, new();
}
