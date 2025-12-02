using Microsoft.AspNetCore.Mvc;
using System;

namespace Zetatech.Accelerate.AspNet.Abstractions;

/// <summary>
/// Represents a base class for implementing custom api controllers in ASP.NET Core MVC.
/// </summary>
[ApiController]
public abstract class BaseApiController : ControllerBase, IDisposable
{
    private Boolean _disposed;

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
