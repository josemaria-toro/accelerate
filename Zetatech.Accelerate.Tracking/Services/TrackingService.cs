using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Services;
using Zetatech.Accelerate.Tracking.Abstractions;
using Zetatech.Accelerate.Tracking.Publishers;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Tracking.Services;

/// <summary>
/// Provides the interface for implementing custom tracking services.
/// </summary>
public sealed class TrackingService : BaseTrackingService<TrackingServiceOptions>
{
    private Boolean _disposed;
    private IPublisherService<TrackingMessage> _diagnosticsPublisherService;
    private IPublisherService<TrackingMessage> _eventsPublisherService;
    private IPublisherService<TrackingMessage> _telemetryPublisherService;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the telemetry service.
    /// </param>
    public TrackingService(IOptions<TrackingServiceOptions> options) : base(options)
    {
        var diagnosticsPublisherServiceOptions = OptionsBuilder.Create(new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = "tracking",
            RoutingKey = "diagnostics"
        });

        _diagnosticsPublisherService = new DiagnosticsPublisherService(diagnosticsPublisherServiceOptions);

        var eventsPublisherServiceOptions = OptionsBuilder.Create(new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = "tracking",
            RoutingKey = "events"
        });

        _eventsPublisherService = new EventsPublisherService(eventsPublisherServiceOptions);

        var telemetryPublisherServiceOptions = OptionsBuilder.Create(new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = "tracking",
            RoutingKey = "telemetry"
        });

        _telemetryPublisherService = new TelemetryPublisherService(telemetryPublisherServiceOptions);
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
            _diagnosticsPublisherService = null;
            _eventsPublisherService = null;
            _telemetryPublisherService = null;
        }
    }
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public override void Track(Dependency dependency)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.Dependency,
            OperationId = dependency.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "duration", $"{dependency.Duration.TotalMilliseconds:0.00000}" },
                { "input", dependency.InputData },
                { "name", dependency.Name },
                { "output", dependency.OutputData },
                { "success", $"{dependency.Success}".ToLowerInvariant() },
                { "target", dependency.TargetName },
                { "type", dependency.Type }
            }
        };

        _telemetryPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public override void Track(Event @event)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.Event,
            OperationId = @event.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "metadata", @event.Metadata },
                { "name", @event.Name }
            }
        };

        _eventsPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="httpRequest">
    /// The HTTP request data to track.
    /// </param>
    public override void Track(HttpRequest httpRequest)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.HttpRequest,
            OperationId = httpRequest.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "body", $"{httpRequest.Body}" },
                { "duration", $"{httpRequest.Duration.TotalMilliseconds:0.00000}" },
                { "ipAddress", $"{httpRequest.IpAddress}" },
                { "name", httpRequest.Name },
                { "responseBody", $"{httpRequest.ResponseBody}" },
                { "responseCode", $"{httpRequest.ResponseCode}" },
                { "success", $"{httpRequest.Success}".ToLowerInvariant() },
                { "uri", $"{httpRequest.Uri}" }
            }
        };

        _telemetryPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public override void Track(Metric metric)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.Metric,
            OperationId = metric.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "dimension", metric.DimensionName },
                { "name", metric.Name },
                { "value", $"{metric.Value:0.00000}" }
            }
        };

        _telemetryPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public override void Track(PageView pageView)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.PageView,
            OperationId = pageView.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "device", pageView.DeviceName },
                { "duration", $"{pageView.Duration.TotalMilliseconds:0.00000}" },
                { "ipAddress", $"{pageView.IpAddress}" },
                { "name", pageView.Name },
                { "uri", $"{pageView.Uri}" },
                { "userAgent", $"{pageView.UserAgent}" }
            }
        };

        _telemetryPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified test result information.
    /// </summary>
    /// <param name="testResult">
    /// The test result to track.
    /// </param>
    public override void Track(TestResult testResult)
    {
        var message = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.TestResult,
            OperationId = testResult.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "duration", $"{testResult.Duration.TotalMilliseconds:0.00000}" },
                { "message", testResult.Message },
                { "name", testResult.Name },
                { "success", $"{testResult.Success}".ToLowerInvariant() }
            }
        };

        _telemetryPublisherService.Publish(message);
    }
    /// <summary>
    /// Tracks the specified trace information.
    /// </summary>
    /// <param name="trace">
    /// The trace data to track.
    /// </param>
    public override void Track(Trace trace)
    {
        var traceMessage = new TrackingMessage
        {
            MessageType = TrackingMessageTypes.Trace,
            OperationId = trace.OperationId,
            Properties = new Dictionary<String, String>
            {
                { "appId", $"{Options.AppId}" },
                { "message", trace.Message },
                { "severity", $"{trace.Severity}" },
                { "sourceTypeName", trace.SourceTypeName }
            }
        };

        _diagnosticsPublisherService.Publish(traceMessage);

        if (trace.Exception != null)
        {
            var exception = trace.Exception;

            while (exception != null)
            {
                var errorMessage = new TrackingMessage
                {
                    MessageType = TrackingMessageTypes.Error,
                    OperationId = trace.OperationId,
                    Properties = new Dictionary<String, String>
                    {
                        { "appId", $"{Options.AppId}" },
                        { "errorTypeName", $"{exception.GetType().Name}" },
                        { "message", exception.Message },
                        { "severity", $"{trace.Severity}" },
                        { "sourceTypeName", trace.SourceTypeName },
                        { "stackTrace", exception.StackTrace }
                    }
                };

                _diagnosticsPublisherService.Publish(errorMessage);

                exception = exception.InnerException;
            }
        }
    }
}
