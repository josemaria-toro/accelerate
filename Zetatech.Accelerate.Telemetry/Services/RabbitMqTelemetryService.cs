using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Publishers;
using Zetatech.Accelerate.Telemetry.Messages;
using Zetatech.Accelerate.Telemetry.Publishers;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Telemetry.Services;

/// <summary>
/// Represents an implementation for a custom telemetry service.
/// </summary>
public sealed class RabbitMqTelemetryService : BaseTelemetryService<RabbitMqTelemetryServiceOptions>
{
    private IPublisherService<DependencyMessage> _dependenciesPublisher;
    private Boolean _disposed;
    private IPublisherService<EventMessage> _eventsPublisher;
    private IPublisherService<MetricMessage> _metricsPublisher;
    private IPublisherService<PageViewMessage> _pageViewsPublisher;
    private IPublisherService<RequestMessage> _requestsPublisher;
    private IPublisherService<TestMessage> _testsPublisher;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the telemetry service.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    public RabbitMqTelemetryService(IOptions<RabbitMqTelemetryServiceOptions> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
        var dependenciesPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _dependenciesPublisher = new DependenciesPublisherService(OptionsBuilder.Create(dependenciesPublisherServiceOptions), loggerFactory);

        var eventsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _eventsPublisher = new EventsPublisherService(OptionsBuilder.Create(eventsPublisherServiceOptions), loggerFactory);

        var metricsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _metricsPublisher = new MetricsPublisherService(OptionsBuilder.Create(metricsPublisherServiceOptions), loggerFactory);

        var pageViewsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _pageViewsPublisher = new PageViewsPublisherService(OptionsBuilder.Create(pageViewsPublisherServiceOptions), loggerFactory);

        var requestsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _requestsPublisher = new RequestsPublisherService(OptionsBuilder.Create(requestsPublisherServiceOptions), loggerFactory);

        var testsPublisherServiceOptions = new RabbitMqPublisherServiceOptions
        {
            ConnectionString = Options.ConnectionString,
            Exchange = Options.Exchange,
            RoutingKey = Options.AvailabilityRk
        };

        _testsPublisher = new TestsPublisherService(OptionsBuilder.Create(testsPublisherServiceOptions), loggerFactory);
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
            _dependenciesPublisher = null;
            _eventsPublisher = null;
            _metricsPublisher = null;
            _pageViewsPublisher = null;
            _requestsPublisher = null;
            _testsPublisher = null;
        }
    }
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public override void Track(IDependency dependency)
    {
        if (dependency == null)
        {
            throw new ArgumentException("The dependency information to track cannot be null", nameof(dependency));
        }

        _dependenciesPublisher.Publish(new DependencyMessage
        {
            AppId = Options.AppId,
            Duration = dependency.Duration.TotalMilliseconds,
            InputData = dependency.InputData,
            Name = dependency.Name,
            OperationId = dependency.OperationId,
            OutputData = dependency.OutputData,
            Success = dependency.Success,
            Target = dependency.Target,
            Type = dependency.Type
        });
    }
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public override void Track(IEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentException("The event information to track cannot be null", nameof(@event));
        }

        _eventsPublisher.Publish(new EventMessage
        {
            AppId = Options.AppId,
            Name = @event.Name,
            OperationId = @event.OperationId
        });
    }
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public override void Track(IMetric metric)
    {
        if (metric == null)
        {
            throw new ArgumentException("The metric information to track cannot be null", nameof(metric));
        }

        _metricsPublisher.Publish(new MetricMessage
        {
            AppId = Options.AppId,
            Dimension = metric.Dimension,
            Name = metric.Name,
            OperationId = metric.OperationId,
            Value = metric.Value
        });
    }
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public override void Track(IPageView pageView)
    {
        if (pageView == null)
        {
            throw new ArgumentException("The page view information to track cannot be null", nameof(pageView));
        }

        _pageViewsPublisher.Publish(new PageViewMessage
        {
            AppId = Options.AppId,
            Duration = pageView.Duration.TotalMilliseconds,
            Name = pageView.Name,
            OperationId = pageView.OperationId,
            Uri = pageView.Uri
        });
    }
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="request">
    /// The HTTP request data to track.
    /// </param>
    public override void Track(IRequest request)
    {
        if (request == null)
        {
            throw new ArgumentException("The HTTP request information to track cannot be null", nameof(request));
        }

        _requestsPublisher.Publish(new RequestMessage
        {
            AppId = Options.AppId,
            Duration = request.Duration.TotalMilliseconds,
            IpAddress = request.IpAddress,
            Name = request.Name,
            OperationId = request.OperationId,
            ResponseCode = request.ResponseCode,
            Success = request.Success,
            Uri = request.Uri
        });
    }
    /// <summary>
    /// Tracks the specified test information.
    /// </summary>
    /// <param name="test">
    /// The test data to track.
    /// </param>
    public override void Track(ITest test)
    {
        if (test == null)
        {
            throw new ArgumentException("The availability test information to track cannot be null", nameof(test));
        }

        _testsPublisher.Publish(new TestMessage
        {
            AppId = Options.AppId,
            Duration = test.Duration.TotalMilliseconds,
            Message = test.Message,
            Name = test.Name,
            OperationId = test.OperationId,
            Success = test.Success
        });
    }
}