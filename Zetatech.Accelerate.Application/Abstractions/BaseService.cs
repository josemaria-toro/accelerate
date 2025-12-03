using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Application.Abstractions;

/// <summary>
/// Represents the base class for implementing custom application services.
/// </summary>
public abstract class BaseService : IService
{
    private Boolean _disposed;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseService(ITrackingService trackingService = null)
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
}