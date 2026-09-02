using System;
using System.Linq;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Domain.Abstractions;

namespace Zetatech.Accelerate.Domain.Specifications;

public sealed class AnySpecification<TEntity> : BaseSpecification<TEntity> where TEntity : class, IEntity, new()
{
    private readonly ISpecification<TEntity>[] _specifications;

    public AnySpecification(params ISpecification<TEntity>[] specifications)
    {
        if (specifications == null || specifications.Length < 2)
        {
            throw new ArgumentException("At least two specifications must be provided", nameof(specifications));
        }

        _specifications = specifications;
    }

    public override Boolean IsSatisfiedBy(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity must be a valid instance", nameof(entity));
        }

        return _specifications.Any(x => x.IsSatisfiedBy(entity));
    }
}
