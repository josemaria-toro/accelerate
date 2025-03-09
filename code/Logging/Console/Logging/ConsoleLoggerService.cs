using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Logging;

/// <summary>
/// Represent a type to perform logging based on system console.
/// </summary>
public sealed class ConsoleLoggerService : LoggerService<ConsoleLoggerServiceOptions>
{
    private Boolean _disposed;
    private SemaphoreSlim _semaphore;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger service.
    /// </param>
    public ConsoleLoggerService(IOptions<ConsoleLoggerServiceOptions> options) : base(options)
    {
        _semaphore = new SemaphoreSlim(1, 1);
    }
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger service.
    /// </param>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    public ConsoleLoggerService(IOptions<ConsoleLoggerServiceOptions> options, String categoryName) : base(options, categoryName)
    {
        _semaphore = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        base.Dispose(disposing);

        if (disposing)
        {
            _semaphore.Dispose();
            _semaphore = null;
        }

        _disposed = true;
    }
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of the object to be written.
    /// </typeparam>
    /// <param name="logLevel">
    /// Entry will be written on this level.
    /// </param>
    /// <param name="eventId">
    /// Id of the event.
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
    public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
    {
        Task.Run(() =>
        {
            if (IsEnabled(logLevel))
            {
                var dateTime = DateTime.UtcNow;
                var template = $"{dateTime:o}~{Options.AppName}~{CategoryName}~{logLevel}~{eventId.Id}~{eventId.Name}";
                var stringBuilder = new StringBuilder();

                if (formatter == null)
                {
                    stringBuilder.AppendLine($"{template}~{state}");
                }
                else
                {
                    stringBuilder.AppendLine($"{template}~{formatter.Invoke(state, exception)}");
                }

                while (exception != null)
                {
                    var stackTrace = exception.StackTrace.Replace("\n", String.Empty)
                                                         .Replace("\r", String.Empty)
                                                         .Replace("\t", String.Empty);

                    stringBuilder.AppendLine($"{template}~{exception.GetType().Name}~{exception.Message}");
                    stringBuilder.AppendLine($"{template}~{exception.GetType().Name}~{stackTrace}");

                    exception = exception.InnerException;
                }

                var message = stringBuilder.ToString();

                _semaphore.Wait();

                try
                {
                    Console.ForegroundColor = logLevel switch
                    {
                        LogLevel.Critical => Options.CriticalColor,
                        LogLevel.Debug => Options.DebugColor,
                        LogLevel.Error => Options.ErrorColor,
                        LogLevel.Warning => Options.WarningColor,
                        _ => Options.InformationColor,
                    };
                    Console.Write(message);
                    Console.ResetColor();
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        });
    }
}