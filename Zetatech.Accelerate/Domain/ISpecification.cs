using System;

namespace Zetatech.Accelerate.Domain;

public interface ISpecification<TEntity> : IDisposable where TEntity : IEntity
{
    Boolean IsSatisfiedBy(TEntity entity);
}