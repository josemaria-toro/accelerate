using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Accelerate.Logging;

/// <summary>
/// Represent a type to perform logging based on azure application insights.
/// </summary>
public sealed class AppInsightsLoggerService : LoggerService<AppInsightsLoggerServiceOptions>
{
    private Boolean _disposed;
    private TelemetryClient _telemetryClient;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger service.
    /// </param>
    public AppInsightsLoggerService(IOptions<AppInsightsLoggerServiceOptions> options) : base(options)
    {
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();

        if (!String.IsNullOrEmpty(Options.ConnectionString))
        {
            telemetryConfiguration.ConnectionString = Options.ConnectionString;
        }

        _telemetryClient = new TelemetryClient(telemetryConfiguration);
        _telemetryClient.Context.Cloud.RoleName = Options.AppName;
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
    public AppInsightsLoggerService(IOptions<AppInsightsLoggerServiceOptions> options, String categoryName) : base(options, categoryName)
    {
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();

        if (!String.IsNullOrEmpty(Options.ConnectionString))
        {
            telemetryConfiguration.ConnectionString = Options.ConnectionString;
        }

        _telemetryClient = new TelemetryClient(telemetryConfiguration);
        _telemetryClient.Context.Cloud.RoleName = Options.AppName;
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
            _telemetryClient = null;
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
    public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        Task.Run(() =>
        {
            if (IsEnabled(logLevel))
            {
                var message = formatter == null ? $"{state}" : formatter.Invoke(state, exception);
                var severity = SeverityLevel.Warning;

                switch (logLevel)
                {
                    case LogLevel.Critical:
                        severity = SeverityLevel.Critical;
                        break;
                    case LogLevel.Debug:
                        severity = SeverityLevel.Verbose;
                        break;
                    case LogLevel.Error:
                        severity = SeverityLevel.Error;
                        break;
                    case LogLevel.Information:
                        severity = SeverityLevel.Information;
                        break;
                }

                _telemetryClient.TrackTrace(message, severity);

                if (exception != null)
                {
                    _telemetryClient.TrackException(exception);

                    while (exception != null)
                    {
                        _telemetryClient.TrackTrace(exception.Message, severity);
                        exception = exception.InnerException;
                    }
                }

                _telemetryClient.Flush();
            }
        });
    }
}