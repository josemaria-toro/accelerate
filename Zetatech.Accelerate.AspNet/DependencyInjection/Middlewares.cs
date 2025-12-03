using Microsoft.AspNetCore.Builder;
using Zetatech.Accelerate.AspNet.Middlewares;

namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Configure the exceptions handling middleware.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseExceptionsMiddleware(this WebApplication webApplication)
    {
        webApplication.UseMiddleware<ExceptionsMiddleware>();

        return webApplication;
    }
    /// <summary>
    /// Configure the tracking middleware.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseTrackingMiddleware(this WebApplication webApplication)
    {
        webApplication.UseMiddleware<TrackingMiddleware>();

        return webApplication;
    }
}
