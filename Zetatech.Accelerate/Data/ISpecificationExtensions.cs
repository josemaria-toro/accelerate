﻿using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Data;

/// <summary>
/// Extensions methods for the <see cref="ISpecification{TEntity}"/> interface.
/// </summary>
public static class ISpecificationExtensions
{
    /// <summary>
    /// Determines whether the specified entity satisfies the specification criteria.
    /// </summary>
    /// <param name="specification">
    /// The specification instance.
    /// </param>
    /// <param name="entity">
    /// The entity to evaluate.
    /// </param>
    /// <returns>
    /// <c>true</c> if the entity satisfies the specification; otherwise, <c>false</c>.
    /// </returns>
    public static async Task<Boolean> IsSatisfiedByAsync<TEntity>(ISpecification<TEntity> specification, TEntity entity) where TEntity : IEntity
    {
        return await Task.FromResult(specification.IsSatisfiedBy(entity));
    }
}