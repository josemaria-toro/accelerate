using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the application events.
/// </summary>
public interface IEventsService : IApplicationService
{
    /// <summary>
    /// Save the information of an application event.
    /// </summary>
    /// <param name="event">
    /// Application event information.
    /// </param>
    void Save(EventDto @event);
}