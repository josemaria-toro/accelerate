using System;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Data;

namespace Zetatech.Accelerate.Domain;

public interface ISpecification<TEntity> : IDisposable where TEntity : class, IEntity, new()
{
    Boolean IsSatisfiedBy(TEntity entity);
    Task<Boolean> IsSatisfiedByAsync(TEntity entity,
                                     CancellationToken cancellationToken = default);
}