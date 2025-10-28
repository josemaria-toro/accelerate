using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Represents the base class for implementing custom logging factories.
/// </summary>
public abstract class BaseLoggingFactory<TOptions> : BaseDisposable, ILoggingFactory where TOptions : BaseLoggingFactoryOptions
{
    private Boolean _disposed;
    private TOptions _options;
    private List<ILoggerProvider> _providers;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    protected BaseLoggingFactory(IOptions<TOptions> options)
    {
        _options = options?.Value;
        _providers = [];
    }

    /// <summary>
    /// Gets the configuration options.
    /// </summary>
    protected TOptions Options => _options;
    /// <summary>
    /// Gets the logging providers added to the factory.
    /// </summary>
    protected List<ILoggerProvider> Providers => _providers;

    /// <summary>
    /// Adds an instance of logging provider to the logging system.
    /// </summary>
    /// <param name="loggerProvider">
    /// The instance of logging provider to add.
    /// </param>
    public virtual void AddProvider(ILoggerProvider loggerProvider)
    {
        if (!_providers.Contains(loggerProvider))
        {
            _providers.Add(loggerProvider);
        }
    }
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
            _options = null;
            _providers = null;
        }
    }
}
