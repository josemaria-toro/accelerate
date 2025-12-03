using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Domain.Abstractions;

/// <summary>
/// Represents the base class for implementing custom specifications for evaluating whether an entity of type <typeparamref name="TEntity"/> satisfies certain criteria.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity to be evaluated.
/// </typeparam>
public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
{
    private Boolean _disposed;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseSpecification(ITrackingService trackingService = null)
    {
        _trackingService = trackingService;
    }

    /// <summary>
    /// Gets the service for tracking application data.
    /// </summary>
    protected ITrackingService TrackingService => _trackingService;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
    }
    /// <summary>
    /// Determines whether the specified entity satisfies the specification criteria.
    /// </summary>
    /// <param name="entity">
    /// The entity to evaluate.
    /// </param>
    public abstract Boolean IsSatisfiedBy(TEntity entity);
}