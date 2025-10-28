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
    private ILoggerFactory _loggerFactory;

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    public ILoggerFactory LoggerFactory
    {
        get => _loggerFactory;
        set
        {
            _loggerFactory = value;
            _logger = _loggerFactory?.CreateLogger(GetType().Name);
        }
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

        if (disposing)
        {
            _logger = null;
        }
    }
}