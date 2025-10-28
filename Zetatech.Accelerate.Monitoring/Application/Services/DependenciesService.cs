using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the application dependencies.
/// </summary>
public interface IDependenciesService : IApplicationService
{
    /// <summary>
    /// Save the information of an application dependency.
    /// </summary>
    /// <param name="dependency">
    /// Application dependency information.
    /// </param>
    void Save(DependencyDto dependency);
}