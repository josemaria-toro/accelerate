using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Logging.Messages;
using Zetatech.Accelerate.Logging.Publishers;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Publishers;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Services;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based logging service.
/// </summary>
public sealed class RabbitMqLoggingService : BaseLoggingService<RabbitMqLoggingServiceOptions>
{
    private Boolean _disposed;
    private IPublisherService<ErrorMessage> _errorsPublisher;
    private IPublisherService<TraceMessage> _tracesPublisher;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the logging service.
    /// </param>
    /// <param name="categoryName">
    /// The category name for the logger.
    /// </param>
    public RabbitMqLoggingService(IOptions<RabbitMqLoggingServiceOptions> options, String categoryName) : base(options, categoryName)
    {
        var errorsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.ErrorsRk
        };

        _errorsPublisher = new ErrorsPublisherService(OptionsBuilder.Create(errorsPublisherServiceOptions));

        var tracesPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.TracesRk
        };

        _tracesPublisher = new TracesPublisherService(OptionsBuilder.Create(tracesPublisherServiceOptions));
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
            _errorsPublisher = null;
            _tracesPublisher = null;
        }
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

            _tracesPublisher.Publish(new TraceMessage
            {
                AppId = Options.AppId,
                Category = Category,
                Device = Environment.MachineName,
                Event = eventId.Name,
                Message = $"{state}",
                OperationId = operationId,
                OperatingSystem = Environment.OSVersion.VersionString,
                Platform = Platform,
                SeverityLevel = logLevel,
            });

            while (exception != null)
            {
                _errorsPublisher.Publish(new ErrorMessage
                {
                    AppId = Options.AppId,
                    Category = Category,
                    Device = Environment.MachineName,
                    Event = eventId.Name,
                    Message = exception.Message,
                    OperationId = operationId,
                    OperatingSystem = Environment.OSVersion.VersionString,
                    Platform = Platform,
                    SeverityLevel = logLevel,
                    StackTrace = exception.StackTrace,
                    TypeName = $"{exception.GetType()}"
                });

                exception = exception.InnerException;
            }
        }
    }
}