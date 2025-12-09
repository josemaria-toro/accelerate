using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Tracking;

/// <summary>
/// Provides the interface for implementing custom tracking services.
/// </summary>
public interface ITrackingService : IDisposable
{
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    Task TrackAsync(Dependency dependency);
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    Task TrackAsync(Event @event);
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="httpRequest">
    /// The HTTP request data to track.
    /// </param>
    Task TrackAsync(HttpRequest httpRequest);
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    Task TrackAsync(Metric metric);
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    Task TrackAsync(PageView pageView);
    /// <summary>
    /// Tracks the specified test result information.
    /// </summary>
    /// <param name="testResult">
    /// The test result to track.
    /// </param>
    Task TrackAsync(TestResult testResult);
    /// <summary>
    /// Tracks the specified trace information.
    /// </summary>
    /// <param name="trace">
    /// The trace data to track.
    /// </param>
    Task TrackAsync(Trace trace);
}
