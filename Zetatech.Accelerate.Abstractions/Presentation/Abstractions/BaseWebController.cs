using Microsoft.AspNetCore.Mvc;
using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Presentation.Abstractions;

/// <summary>
/// Represents a base class for implementing custom web controllers in ASP.NET Core MVC.
/// </summary>
public abstract class BaseWebController : Controller, IDisposable
{
    private Boolean _disposed;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseWebController(ITrackingService trackingService = null)
    {
        _trackingService = trackingService;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose(disposing);
    }
}
