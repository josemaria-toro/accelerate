using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the application metrics.
/// </summary>
public interface IMetricsService : IApplicationService
{
    /// <summary>
    /// Save the information of an application metric.
    /// </summary>
    /// <param name="metric">
    /// Application metric information.
    /// </param>
    void Save(MetricDto metric);
}