using Microsoft.AspNetCore.Builder;
using Zetatech.Accelerate.Web.Middlewares;

namespace Zetatech.Accelerate.Web.Extensions;

/// <summary>
/// Extensions methods for the <see cref="IApplicationBuilder"/> interface.
/// </summary>
public static class IApplicationBuilderExtensions
{
    /// <summary>
    /// Adds and configure the available middlewares in the ASP.NET request pipeline.
    /// </summary>
    /// <param name="applicationBuilder">
    /// The instance of the application builder.
    /// </param>
    public static void ConfigureMiddlewares(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<CorrelationMiddleware>();
        applicationBuilder.UseMiddleware<TrackRequestMiddleware>();
        applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
