using System.Threading.Tasks;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Extensions methods for the <see cref="ITelemetryService"/> interface.
/// </summary>
public static class ITelemetryServiceExtension
{
    /// <summary>
    /// Tracks the specified dependency information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="dependency">
    /// The dependency data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, IDependency dependency)
    {
        await Task.Run(() => telemetryService.Track(dependency));
    }
    /// <summary>
    /// Tracks the specified event information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="event">
    /// The event data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, IEvent @event)
    {
        await Task.Run(() => telemetryService.Track(@event));
    }
    /// <summary>
    /// Tracks the specified metric information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="metric">
    /// The metric data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, IMetric metric)
    {
        await Task.Run(() => telemetryService.Track(metric));
    }
    /// <summary>
    /// Tracks the specified page view information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="pageView">
    /// The page view data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, IPageView pageView)
    {
        await Task.Run(() => telemetryService.Track(pageView));
    }
    /// <summary>
    /// Tracks the specified HTTP request information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="request">
    /// The HTTP request data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, IRequest request)
    {
        await Task.Run(() => telemetryService.Track(request));
    }
    /// <summary>
    /// Tracks the specified test information.
    /// </summary>
    /// <param name="telemetryService">
    /// The instance of the telemetry service.
    /// </param>
    /// <param name="test">
    /// The test data to track.
    /// </param>
    public static async Task TrackAsync(this ITelemetryService telemetryService, ITest test)
    {
        await Task.Run(() => telemetryService.Track(test));
    }
}