namespace Zetatech.Accelerate.Infrastructure.Tracking;

/// <summary>
/// Defines the types of tracking messages.
/// </summary>
public enum TrackingMessageTypes
{
    /// <summary>
    /// Dependency tracking message.
    /// </summary>
    Dependency = 1,
    /// <summary>
    /// Error tracking message.
    /// </summary>
    Error = 2,
    /// <summary>
    /// Event tracking message.
    /// </summary>
    Event = 3,
    /// <summary>
    /// HTTP request tracking message.
    /// </summary>
    HttpRequest = 4,
    /// <summary>
    /// Metric tracking message.
    /// </summary>
    Metric = 5,
    /// <summary>
    /// Page view tracking message.
    /// </summary>
    PageView = 6,
    /// <summary>
    /// Test result tracking message.
    /// </summary>
    TestResult = 7,
    /// <summary>
    /// Trace tracking message.
    /// </summary>
    Trace = 8
}
