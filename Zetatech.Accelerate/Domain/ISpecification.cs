using System;

namespace Zetatech.Accelerate.Domain;

/// <summary>
/// Defines a specification interface for evaluating whether an entity of type <typeparamref name="TEntity"/> satisfies certain criteria.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity to be evaluated.
/// </typeparam>
public interface ISpecification<TEntity> : IDisposable where TEntity : IEntity
{
    /// <summary>
    /// Determines whether the specified entity satisfies the specification criteria.
    /// </summary>
    /// <param name="entity">
    /// The entity to evaluate.
    /// </param>
    Boolean IsSatisfiedBy(TEntity entity);
}