using Microsoft.AspNetCore.Mvc;
using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.AspNet.Abstractions;

/// <summary>
/// Represents a base class for implementing custom api controllers in ASP.NET Core MVC.
/// </summary>
[ApiController]
public abstract class BaseApiController : ControllerBase, IDisposable
{
    private Boolean _disposed;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseApiController(ITrackingService trackingService = null)
    {
        _trackingService = trackingService;
    }

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
