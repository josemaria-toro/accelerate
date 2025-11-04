using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents a base class for implementing custom telemetry services.
/// </summary>
public abstract class BaseTelemetryService<TOptions> : BaseDisposable, ITelemetryService where TOptions : BaseTelemetryServiceOptions
{
    private Boolean _disposed;
    private ILogger _logger;
    private TOptions _options;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the telemetry service.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    protected BaseTelemetryService(IOptions<TOptions> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
        _options = options?.Value;
    }

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets the options for the telemetry service.
    /// </summary>
    protected TOptions Options => _options;

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
            _logger = null;
            _options = null;
        }
    }
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public abstract void Track(IDependency dependency);
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public abstract void Track(IEvent @event);
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public abstract void Track(IMetric metric);
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public abstract void Track(IPageView pageView);
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="request">
    /// The HTTP request data to track.
    /// </param>
    public abstract void Track(IRequest request);
    /// <summary>
    /// Tracks the specified test information.
    /// </summary>
    /// <param name="test">
    /// The test data to track.
    /// </param>
    public abstract void Track(ITest test);
}