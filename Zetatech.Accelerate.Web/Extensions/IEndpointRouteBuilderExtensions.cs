using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Zetatech.Accelerate.Web.Extensions;

/// <summary>
/// Extensions methods for the <see cref="IEndpointRouteBuilder"/> interface.
/// </summary>
public static class IEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Adds and configure the available controllers in the ASP.NET endpoint route builder.
    /// </summary>
    /// <param name="endpointRouteBuilder">
    /// The instance of the endpoint route builder.
    /// </param>
    public static void ConfigureControllers(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllers();
    }
}
