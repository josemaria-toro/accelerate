using System;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Data;

namespace Zetatech.Accelerate.Domain;

public static class ISpecificationAsync
{
    public static async Task<Boolean> IsSatisfiedByAsync<TEntity>(this ISpecification<TEntity> specification,
                                                                  TEntity entity,
                                                                  CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
    {
        return await Task.Run(() => specification.IsSatisfiedBy(entity), cancellationToken);
    }
}