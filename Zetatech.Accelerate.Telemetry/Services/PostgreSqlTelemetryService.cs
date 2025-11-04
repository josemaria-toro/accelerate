using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Telemetry.Entities;
using Zetatech.Accelerate.Telemetry.Repositories;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Telemetry.Services;

/// <summary>
/// Represents an implementation for a custom PostgeSQL-based telemetry service.
/// </summary>
public sealed class PostgreSqlTelemetryService : BaseTelemetryService<PostgreSqlTelemetryServiceOptions>
{
    private IRepository<DependencyEntity> _dependenciesRepository;
    private Boolean _disposed;
    private IRepository<EventEntity> _eventsRepository;
    private IRepository<MetricEntity> _metricsRepository;
    private IRepository<PageViewEntity> _pageViewsRepository;
    private IRepository<RequestEntity> _requestsRepository;
    private IRepository<TestEntity> _testsRepository;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the telemetry service.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    public PostgreSqlTelemetryService(IOptions<PostgreSqlTelemetryServiceOptions> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
        var postgreSqlRepositoryOptions = new PostgreSqlRepositoryOptions
        {
            ConnectionString = Options.ConnectionString,
            DetailedErrors = Options.DetailedErrors,
            LazyLoading = Options.LazyLoading,
            SensitiveDataLogging = Options.SensitiveDataLogging,
            Timeout = Options.Timeout,
            TrackChanges = Options.TrackChanges
        };

        _dependenciesRepository = new DependenciesRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
        _eventsRepository = new EventsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
        _metricsRepository = new MetricsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
        _pageViewsRepository = new PageViewsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
        _requestsRepository = new RequestsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
        _testsRepository = new TestsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions), loggerFactory);
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
            _dependenciesRepository = null;
            _eventsRepository = null;
            _metricsRepository = null;
            _pageViewsRepository = null;
            _requestsRepository = null;
            _testsRepository = null;
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

        _dependenciesRepository.Insert(new DependencyEntity
        {
            AppId = Options.AppId,
            Duration = dependency.Duration.TotalMilliseconds,
            Id = Guid.NewGuid(),
            InputData = dependency.InputData ?? String.Empty,
            Name = dependency.Name ?? String.Empty,
            OperationId = dependency.OperationId,
            OutputData = dependency.OutputData ?? String.Empty,
            Success = dependency.Success,
            Target = dependency.Target ?? String.Empty,
            Timestamp = DateTime.UtcNow,
            Type = dependency.Type ?? String.Empty
        });

        _dependenciesRepository.Commit();
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

        _eventsRepository.Insert(new EventEntity
        {
            AppId = Options.AppId,
            Id = Guid.NewGuid(),
            Name = @event.Name ?? String.Empty,
            OperationId = @event.OperationId,
            Timestamp = DateTime.UtcNow
        });

        _eventsRepository.Commit();
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

        _metricsRepository.Insert(new MetricEntity
        {
            AppId = Options.AppId,
            Dimension = metric.Dimension ?? String.Empty,
            Id = Guid.NewGuid(),
            Name = metric.Name ?? String.Empty,
            OperationId = metric.OperationId,
            Timestamp = DateTime.UtcNow,
            Value = metric.Value
        });

        _metricsRepository.Commit();
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

        _pageViewsRepository.Insert(new PageViewEntity
        {
            AppId = Options.AppId,
            Duration = pageView.Duration.TotalMilliseconds,
            Id = Guid.NewGuid(),
            Name = pageView.Name ?? String.Empty,
            OperationId = pageView.OperationId,
            Timestamp = DateTime.UtcNow,
            Url = $"{pageView.Uri}"
        });

        _pageViewsRepository.Commit();
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

        _requestsRepository.Insert(new RequestEntity
        {
            AppId = Options.AppId,
            Duration = request.Duration.TotalMilliseconds,
            Id = Guid.NewGuid(),
            IpAddress = $"{request.IpAddress}",
            Name = request.Name ?? String.Empty,
            OperationId = request.OperationId,
            ResponseCode = (Int32)request.ResponseCode,
            Success = request.Success,
            Timestamp = DateTime.UtcNow,
            Url = $"{request.Uri}"
        });

        _requestsRepository.Commit();
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
            throw new ArgumentException("The test information to track cannot be null", nameof(test));
        }

        _testsRepository.Insert(new TestEntity
        {
            AppId = Options.AppId,
            Duration = test.Duration.TotalMilliseconds,
            Id = Guid.NewGuid(),
            Message = test.Message ?? String.Empty,
            Name = test.Name ?? String.Empty,
            OperationId = test.OperationId,
            Success = test.Success,
            Timestamp = DateTime.UtcNow
        });

        _testsRepository.Commit();
    }
}