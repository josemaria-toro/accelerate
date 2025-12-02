using System.Threading.Tasks;

namespace Zetatech.Accelerate.Tracking;

/// <summary>
/// Extensions methods for the <see cref="ITrackingService"/> interface.
/// </summary>
public static class ITrackingServiceExtension
{
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, Dependency dependency)
    {
        await Task.Run(() => trackingService.Track(dependency));
    }
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, Event @event)
    {
        await Task.Run(() => trackingService.Track(@event));
    }
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="httpRequest">
    /// The HTTP request data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, HttpRequest httpRequest)
    {
        await Task.Run(() => trackingService.Track(httpRequest));
    }
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, Metric metric)
    {
        await Task.Run(() => trackingService.Track(metric));
    }
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, PageView pageView)
    {
        await Task.Run(() => trackingService.Track(pageView));
    }
    /// <summary>
    /// Tracks the specified test result information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="testResult">
    /// The test result to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, TestResult testResult)
    {
        await Task.Run(() => trackingService.Track(testResult));
    }
    /// <summary>
    /// Tracks the specified trace information.
    /// </summary>
    /// <param name="trackingService">
    /// The instance of the tracking service.
    /// </param>
    /// <param name="trace">
    /// The trace data to track.
    /// </param>
    public static async Task TrackAsync(this ITrackingService trackingService, Trace trace)
    {
        await Task.Run(() => trackingService.Track(trace));
    }
}