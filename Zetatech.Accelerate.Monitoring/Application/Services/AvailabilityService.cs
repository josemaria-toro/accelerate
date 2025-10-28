using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the results of availability tests.
/// </summary>
public interface IAvailabilityService : IApplicationService
{
    /// <summary>
    /// Save the result of an availability test.
    /// </summary>
    /// <param name="availability">
    /// Availability y test information.
    /// </param>
    void Save(AvailabilityDto availability);
}