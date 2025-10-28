using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Factories;

/// <summary>
/// Represents an implementation for a custom logging factory.
/// </summary>
public sealed class LoggingFactory : BaseLoggingFactory<LoggingFactoryOptions>, ILoggingService
{
    private Boolean _disposed;
    private IEnumerable<ILogger> _loggers;
    private ParallelOptions _parallelOptions;
    private IExternalScopeProvider _scopeProvider;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    public LoggingFactory(IOptions<LoggingFactoryOptions> options) : this(options, [])
    {
    }
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    /// <param name="loggers">
    /// The loggers collection to be used on logger mode.
    /// </param>
    public LoggingFactory(IOptions<LoggingFactoryOptions> options, IEnumerable<ILogger> loggers) : base(options)
    {
        _loggers = loggers;
        _parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        _scopeProvider = new LoggerExternalScopeProvider();
    }

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
    public IDisposable BeginScope<TState>(TState state)
    {
        return _scopeProvider.Push(state);
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
    public override ILogger CreateLogger(String categoryName)
    {
        if (String.IsNullOrEmpty(categoryName))
        {
            throw new ArgumentException("The category cannot be null or empty", nameof(categoryName));
        }

        var options = OptionsBuilder.Create(Options);
        var providers = Providers.Select(x => x.CreateLogger(categoryName));

        return new LoggingFactory(options, providers);
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
    public Boolean IsEnabled(LogLevel logLevel) => true;
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
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
    {
        if (IsEnabled(logLevel) && _loggers.Any())
        {
            Parallel.ForEach(_loggers, _parallelOptions, logger =>
            {
                logger.Log(logLevel, eventId, state, exception, formatter);
            });
        }
    }
}
