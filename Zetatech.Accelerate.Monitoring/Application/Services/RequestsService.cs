using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the HTTP requests.
/// </summary>
public interface IRequestsService : IApplicationService
{
    /// <summary>
    /// Save the information of an HTTP request.
    /// </summary>
    /// <param name="request">
    /// HTTP request information.
    /// </param>
    void Save(RequestDto request);
}