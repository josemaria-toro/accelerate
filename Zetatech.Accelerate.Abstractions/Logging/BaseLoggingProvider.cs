using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Represents the base class for implementing custom logging providers.
/// </summary>
public abstract class BaseLoggingProvider<TOptions> : BaseDisposable, ILoggingProvider where TOptions : BaseLoggingProviderOptions
{
    private Boolean _disposed;
    private ConcurrentDictionary<String, ILogger> _loggers;
    private TOptions _options;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    protected BaseLoggingProvider(IOptions<TOptions> options)
    {
        _loggers = new ConcurrentDictionary<String, ILogger>();
        _options = options?.Value;
    }

    /// <summary>
    /// Gets the loggers already created.
    /// </summary>
    protected ConcurrentDictionary<String, ILogger> Loggers => _loggers;
    /// <summary>
    /// Gets the configuration options.
    /// </summary>
    protected TOptions Options => _options;

    /// <summary>
    /// Creates a new logger instance for the specified category.
    /// </summary>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    /// <returns>
    /// An <see cref="ILogger"/> instance.
    /// </returns>
    public abstract ILogger CreateLogger(String categoryName);
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
            _loggers = null;
            _options = null;
        }
    }
}