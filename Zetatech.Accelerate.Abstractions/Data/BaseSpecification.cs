using System;

namespace Zetatech.Accelerate.Data;

/// <summary>
/// Represents the base class for implementing custom specifications for evaluating whether an entity of type <typeparamref name="TEntity"/> satisfies certain criteria.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity to be evaluated.
/// </typeparam>
public abstract class BaseSpecification<TEntity> : BaseDisposable where TEntity : IEntity
{
    /// <summary>
    /// Determines whether the specified entity satisfies the specification criteria.
    /// </summary>
    /// <param name="entity">
    /// The entity to evaluate.
    /// </param>
    /// <returns>
    /// <c>true</c> if the entity satisfies the specification; otherwise, <c>false</c>.
    /// </returns>
    public abstract Boolean IsSatisfiedBy(TEntity entity);
}