using System;
using Zetatech.Accelerate.Data;

namespace Zetatech.Accelerate.Domain;

public interface ISpecification<TEntity> : IDisposable where TEntity : class, IEntity, new()
{
    Boolean IsSatisfiedBy(TEntity entity);
}