using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Services;

/// <summary>
/// Provides the interface for implementing custom services for managing the page views.
/// </summary>
public interface IPageViewsService : IApplicationService
{
    /// <summary>
    /// Save the information of a page view.
    /// </summary>
    /// <param name="pageview">
    /// Page view information.
    /// </param>
    void Save(PageViewDto pageview);
}