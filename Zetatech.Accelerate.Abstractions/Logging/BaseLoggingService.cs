using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Represents the base class for implementing custom logging services.
/// </summary>
/// <typeparam name="TOptions">
/// The type of the configuration options for the logging service. Must inherit from <see cref="BaseLoggingServiceOptions"/>.
/// </typeparam>
public abstract class BaseLoggingService<TOptions> : BaseDisposable, ILoggingService where TOptions : BaseLoggingServiceOptions
{
    private String _category;
    private Boolean _disposed;
    private TOptions _options;
    private String _platform;
    private IExternalScopeProvider _scopeProvider;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the logging service.
    /// </param>
    /// <param name="category">
    /// The category name for the logger.
    /// </param>
    protected BaseLoggingService(IOptions<TOptions> options, String category)
    {
        _category = category;
        _options = options.Value;
        _platform = "Unknown";
        _scopeProvider = new LoggerExternalScopeProvider();

        SetPlatformName();
    }

    /// <summary>
    /// Gets the name of the category.
    /// </summary>
    protected String Category => _category;
    /// <summary>
    /// Gets the configuration options.
    /// </summary>
    protected TOptions Options => _options;
    /// <summary>
    /// Gets the name of the device platform.
    /// </summary>
    protected String Platform => _platform;

    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeParam name="TState">
    /// The type of the state to begin scope for.
    /// </typeParam>
    /// <param name="state">
    /// The identifier for the scope.
    /// </param>
    /// <returns>
    /// An <see cref="IDisposable"/> that ends the logical operation scope on dispose.
    /// </returns>
    public virtual IDisposable BeginScope<TState>(TState state)
    {
        return _scopeProvider.Push(state);
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
            _category = null;
            _options = null;
            _platform = null;
            _scopeProvider = null;
        }
    }
    /// <summary>
    /// Determines whether the logger is enabled for the specified log level.
    /// </summary>
    /// <param name="logLevel">
    /// The log level to check.
    /// </param>
    /// <returns>
    /// true if enabled; otherwise, false.
    /// </returns>
    public virtual Boolean IsEnabled(LogLevel logLevel)
    {
        return (logLevel == LogLevel.Critical && _options.Critical) ||
               (logLevel == LogLevel.Debug && _options.Debug) ||
               (logLevel == LogLevel.Error && _options.Error) ||
               (logLevel == LogLevel.Information && _options.Information) ||
               (logLevel == LogLevel.Warning && _options.Warning);
    }
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeParam name="TState">
    /// The type of the object to be written.
    /// </typeParam>
    /// <param name="logLevel">
    /// Entry will be written on this level.
    /// </param>
    /// <param name="eventId">
    /// The event id associated with the log.
    /// </param>
    /// <param name="state">
    /// The entry to be written.
    /// </param>
    /// <param name="exception">
    /// The exception related to this entry.
    /// </param>
    /// <param name="formatter">
    /// Function to create a string message of the state and exception.
    /// </param>
    public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter);

    /// <summary>
    /// Set the platform name based on the current operating system.
    /// </summary>
    private void SetPlatformName()
    {
        if (OperatingSystem.IsAndroid())
        {
            _platform = "Android";
        }
        else if (OperatingSystem.IsBrowser())
        {
            _platform = "Wasm";
        }
        else if (OperatingSystem.IsFreeBSD())
        {
            _platform = "FreeBSD";
        }
        else if (OperatingSystem.IsIOS())
        {
            _platform = "iOS";
        }
        else if (OperatingSystem.IsLinux())
        {
            _platform = "Linux";
        }
        else if (OperatingSystem.IsMacCatalyst())
        {
            _platform = "Mac Catalist";
        }
        else if (OperatingSystem.IsMacOS())
        {
            _platform = "Mac";
        }
        else if (OperatingSystem.IsTvOS())
        {
            _platform = "TV";
        }
        else if (OperatingSystem.IsWasi())
        {
            _platform = "Wasi";
        }
        else if (OperatingSystem.IsWatchOS())
        {
            _platform = "Watch";
        }
        else if (OperatingSystem.IsWindows())
        {
            _platform = "Windows";
        }
    }
}