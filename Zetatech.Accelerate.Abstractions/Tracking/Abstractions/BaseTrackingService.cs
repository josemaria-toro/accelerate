using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Tracking.Abstractions;

/// <summary>
/// Represents a base class for implementing custom tracking services.
/// </summary>
public abstract class BaseTrackingService<TOptions> : ITrackingService where TOptions : BaseTrackingServiceOptions
{
    private Boolean _disposed;
    private TOptions _options;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the telemetry service.
    /// </param>
    protected BaseTrackingService(IOptions<TOptions> options)
    {
        _options = options?.Value;
    }

    /// <summary>
    /// Gets the options for the telemetry service.
    /// </summary>
    protected TOptions Options => _options;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        if (disposing)
        {
            _options = null;
        }
    }
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public abstract Task TrackAsync(Dependency dependency);
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public abstract Task TrackAsync(Event @event);
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="httpRequest">
    /// The HTTP request data to track.
    /// </param>
    public abstract Task TrackAsync(HttpRequest httpRequest);
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public abstract Task TrackAsync(Metric metric);
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public abstract Task TrackAsync(PageView pageView);
    /// <summary>
    /// Tracks the specified test result information.
    /// </summary>
    /// <param name="testResult">
    /// The test result to track.
    /// </param>
    public abstract Task TrackAsync(TestResult testResult);
    /// <summary>
    /// Tracks the specified trace information.
    /// </summary>
    /// <param name="trace">
    /// The trace data to track.
    /// </param>
    public abstract Task TrackAsync(Trace trace);
}