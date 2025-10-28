using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the application errors.
/// </summary>
public interface IErrorsService : IApplicationService
{
    /// <summary>
    /// Save the information of an application error.
    /// </summary>
    /// <param name="error">
    /// Application error information.
    /// </param>
    void Save(ErrorDto error);
}