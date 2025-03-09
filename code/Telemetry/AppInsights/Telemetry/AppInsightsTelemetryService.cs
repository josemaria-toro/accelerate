using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Accelerate.Telemetry;

/// <summary>
/// Telemetry service based on azure application insights.
/// </summary>
public sealed class AppInsightsTelemetryService : TelemetryService<AppInsightsTelemetryServiceOptions>
{
    private Boolean _disposed;
    private TelemetryClient _telemetryClient;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options.
    /// </param>
    public AppInsightsTelemetryService(IOptions<AppInsightsTelemetryServiceOptions> options) : base(options)
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
    /// Track the availablity information of a service or application.
    /// </summary>
    /// <param name="availability">
    /// Service or application availability information.
    /// </param>
    public override void Track(Availability availability)
    {
        if (availability == null)
        {
            throw new ArgumentException("Availability information cannot be null", nameof(availability));
        }

        Task.Run(() =>
        {
            var availabilityTelemetry = new AvailabilityTelemetry
            {
                Duration = availability.Duration,
                Id = $"{Guid.NewGuid()}",
                Message = availability.Message,
                Name = availability.Name,
                Success = availability.Success,
                Timestamp = availability.Timestamp
            };

            foreach (var item in availability.Metadata)
            {
                availabilityTelemetry.Properties.Add(item.Key, $"{item.Value}");
            }

            _telemetryClient.Context.Operation.ParentId = availability.CorrelationId;
            _telemetryClient.TrackAvailability(availabilityTelemetry);
            _telemetryClient.Flush();
        });
    }
    /// <summary>
    /// Track the dependency information of a service or application.
    /// </summary>
    /// <param name="dependency">
    /// Service or application dependency information.
    /// </param>
    public override void Track(Dependency dependency)
    {
        if (dependency == null)
        {
            throw new ArgumentException("Dependency information cannot be null", nameof(dependency));
        }

        Task.Run(() =>
        {
            var dependencyTelemetry = new DependencyTelemetry
            {
                Data = dependency.Data,
                Duration = dependency.Duration,
                Id = $"{Guid.NewGuid()}",
                Name = dependency.Name,
                ResultCode = dependency.Results,
                Success = dependency.Success,
                Target = dependency.Target,
                Timestamp = dependency.Timestamp,
                Type = dependency.Type
            };

            foreach (var item in dependency.Metadata)
            {
                dependencyTelemetry.Properties.Add(item.Key, $"{item.Value}");
            }

            _telemetryClient.Context.Operation.ParentId = dependency.CorrelationId;
            _telemetryClient.TrackDependency(dependencyTelemetry);
            _telemetryClient.Flush();
        });
    }
    /// <summary>
    /// Track a service or application event.
    /// </summary>
    /// <param name="event">
    /// Service or application event information.
    /// </param>
    public override void Track(Event @event)
    {
        if (@event == null)
        {
            throw new ArgumentException("Event information cannot be null", nameof(@event));
        }

        Task.Run(() =>
        {
            var eventTelemetry = new EventTelemetry
            {
                Name = @event.Name,
                Timestamp = @event.Timestamp
            };

            foreach (var item in @event.Metadata)
            {
                eventTelemetry.Properties.Add(item.Key, $"{item.Value}");
            }

            _telemetryClient.Context.Operation.ParentId = @event.CorrelationId;
            _telemetryClient.TrackEvent(eventTelemetry);
            _telemetryClient.Flush();
        });
    }
    /// <summary>
    /// Track a service or application metric.
    /// </summary>
    /// <param name="metric">
    /// Service or application metric information.
    /// </param>
    public override void Track(Metric metric)
    {
        if (metric == null)
        {
            throw new ArgumentException("Metric information cannot be null", nameof(metric));
        }

        Task.Run(() =>
        {
            _telemetryClient.Context.Operation.ParentId = metric.CorrelationId;

            if (String.IsNullOrEmpty(metric.Dimension))
            {
                _telemetryClient.GetMetric(metric.Name)
                                .TrackValue(metric.Value);
            }
            else
            {
                _telemetryClient.GetMetric(metric.Name, metric.Dimension)
                                .TrackValue(metric.Value);
            }

            _telemetryClient.Flush();
        });
    }
    /// <summary>
    /// Track a page view of a user.
    /// </summary>
    /// <param name="pageView">
    /// Page information.
    /// </param>
    public override void Track(PageView pageView)
    {
        if (pageView == null)
        {
            throw new ArgumentException("Page view information cannot be null", nameof(pageView));
        }

        Task.Run(() =>
        {
            var pageViewTelemetry = new PageViewTelemetry
            {
                Duration = pageView.Duration,
                Id = $"{Guid.NewGuid()}",
                Name = pageView.Name,
                Timestamp = pageView.Timestamp,
                Url = pageView.Uri
            };

            foreach (var item in pageView.Metadata)
            {
                pageViewTelemetry.Properties.Add(item.Key, $"{item.Value}");
            }

            _telemetryClient.Context.Operation.ParentId = pageView.CorrelationId;
            _telemetryClient.TrackPageView(pageViewTelemetry);
            _telemetryClient.Flush();
        });
    }
    /// <summary>
    /// Track an http request information.
    /// </summary>
    /// <param name="request">
    /// Http request information.
    /// </param>
    public override void Track(Request request)
    {
        if (request == null)
        {
            throw new ArgumentException("Request information cannot be null", nameof(request));
        }

        Task.Run(() =>
        {
            var requestTelemetry = new RequestTelemetry
            {
                Duration = request.Duration,
                Id = $"{Guid.NewGuid()}",
                Name = request.Name,
                ResponseCode = $"{request.ResponseCode}",
                Source = request.Client,
                Success = request.Success,
                Timestamp = request.Timestamp,
                Url = request.Uri
            };

            foreach (var item in request.Metadata)
            {
                requestTelemetry.Properties.Add(item.Key, $"{item.Value}");
            }

            _telemetryClient.Context.Operation.ParentId = request.CorrelationId;
            _telemetryClient.TrackRequest(requestTelemetry);
            _telemetryClient.Flush();
        });
    }
}