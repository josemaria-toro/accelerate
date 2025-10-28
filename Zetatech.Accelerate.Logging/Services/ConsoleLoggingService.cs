using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Zetatech.Accelerate.Logging.Services;

/// <summary>
/// Represents an implementation for a custom console-based logging service.
/// </summary>
public sealed class ConsoleLoggingService : BaseLoggingService<ConsoleLoggingServiceOptions>
{
    private Boolean _disposed;
    private String _format;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the logging service.
    /// </param>
    /// <param name="categoryName">
    /// The category name for the logger.
    /// </param>
    public ConsoleLoggingService(IOptions<ConsoleLoggingServiceOptions> options, String categoryName) : base(options, categoryName)
    {
        _format = "{timestamp} | {operation} | {severity} | {platform} | {category} | {event} | {message}";
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

        base.Dispose();

        if (disposing)
        {
            _format = null;
        }
    }
    /// <summary>
    /// Formats the log message according to the specified format and log state.
    /// </summary>
    /// <typeParam name="TState">
    /// The type of the state to format.
    /// </typeParam>
    /// <param name="operationId">
    /// The operation identifier used to associate related message.
    /// </param>
    /// <param name="logLevel">
    /// The severity level of the log.
    /// </param>
    /// <param name="eventName">
    /// The name of event.
    /// </param>
    /// <param name="state">
    /// The log state.
    /// </param>
    /// <returns>
    /// The formatted log message.
    /// </returns>
    private String GetFormattedMessage<TState>(Guid operationId, LogLevel logLevel, String eventName, TState state)
    {
        var message = $"{state}".Replace("\n", String.Empty)
                                .Replace("\r", String.Empty)
                                .Replace("\t", String.Empty)
                                .Trim();

        while (message.Contains("  ", StringComparison.InvariantCulture))
        {
            message = message.Replace("  ", " ");
        }

        return _format.Replace("{category}", Category)
                      .Replace("{event}", eventName)
                      .Replace("{message}", message)
                      .Replace("{operation}", $"{operationId}")
                      .Replace("{platform}", Platform)
                      .Replace("{severity}", $"{logLevel}")
                      .Replace("{timestamp}", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
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
    public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
    {
        if (IsEnabled(logLevel))
        {
            var operationId = Guid.NewGuid();

            Console.ForegroundColor = logLevel switch
            {
                LogLevel.Critical => ConsoleColor.DarkRed,
                LogLevel.Debug => ConsoleColor.DarkMagenta,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Warning => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };

            Console.WriteLine(
                GetFormattedMessage(operationId, logLevel, eventId.Name, state)
            );

            while (exception != null)
            {
                Console.WriteLine(
                    GetFormattedMessage(operationId, logLevel, eventId.Name, $"{exception.GetType()} | {exception.Message}")
                );

                Console.WriteLine(
                    GetFormattedMessage(operationId, logLevel, eventId.Name, $"{exception.GetType()} | {exception.StackTrace}")
                );

                exception = exception.InnerException;
            }

            Console.ResetColor();
        }
    }
}