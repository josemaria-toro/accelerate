using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the application traces.
/// </summary>
public interface ITracesService : IApplicationService
{
    /// <summary>
    /// Save the information of an application trace.
    /// </summary>
    /// <param name="trace">
    /// Application trace information.
    /// </param>
    void Save(TraceDto trace);
}