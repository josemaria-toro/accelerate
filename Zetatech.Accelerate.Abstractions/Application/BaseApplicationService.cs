using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Application;

/// <summary>
/// Represents the base class for implementing custom application services.
/// </summary>
public abstract class BaseApplicationService : BaseDisposable, IApplicationService
{
    private Boolean _disposed;
    private ILogger _logger;

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    protected BaseApplicationService(ILoggerFactory loggerFactory = null)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
    }

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;

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

        if (disposing)
        {
            _logger = null;
        }
    }
}