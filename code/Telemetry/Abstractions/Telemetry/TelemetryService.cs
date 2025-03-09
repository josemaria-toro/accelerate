using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Telemetry;

/// <summary>
/// Base class for telemetry services.
/// </summary>
/// <typeparam name="TOptions">
/// Type of configuration options.
/// </typeparam>
public abstract class TelemetryService<TOptions> : ITelemetryService where TOptions : TelemetryServiceOptions
{
    private Boolean _disposed;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options.
    /// </param>
    protected TelemetryService(IOptions<TOptions> options)
    {
        Options = options?.Value ?? throw new ArgumentException("Configuration options cannot be null", nameof(options));
    }

    /// <summary>
    /// Configuration options.
    /// </summary>
    protected TOptions Options { get; private set; }

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
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
    }
    /// <summary>
    /// Track the availablity information of a service or application.
    /// </summary>
    /// <param name="availability">
    /// Service or application availability information.
    /// </param>
    public abstract void Track(Availability availability);
    /// <summary>
    /// Track the dependency information of a service or application.
    /// </summary>
    /// <param name="dependency">
    /// Service or application dependency information.
    /// </param>
    public abstract void Track(Dependency dependency);
    /// <summary>
    /// Track a service or application event.
    /// </summary>
    /// <param name="event">
    /// Service or application event information.
    /// </param>
    public abstract void Track(Event @event);
    /// <summary>
    /// Track a service or application metric.
    /// </summary>
    /// <param name="metric">
    /// Service or application metric information.
    /// </param>
    public abstract void Track(Metric metric);
    /// <summary>
    /// Track a page view of a user.
    /// </summary>
    /// <param name="pageView">
    /// Page information.
    /// </param>
    public abstract void Track(PageView pageView);
    /// <summary>
    /// Track an http request information.
    /// </summary>
    /// <param name="request">
    /// Http request information.
    /// </param>
    public abstract void Track(Request request);
}