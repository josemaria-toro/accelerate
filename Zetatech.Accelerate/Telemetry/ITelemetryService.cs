using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom telemetry services.
/// </summary>
public interface ITelemetryService : IDisposable
{
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    void Track(IDependency dependency);
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    void Track(IEvent @event);
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    void Track(IMetric metric);
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    void Track(IPageView pageView);
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="request">
    /// The HTTP request data to track.
    /// </param>
    void Track(IRequest request);
    /// <summary>
    /// Tracks the specified test information.
    /// </summary>
    /// <param name="test">
    /// The test data to track.
    /// </param>
    void Track(ITest test);
}