using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.AspNetCore.Abstractions;

namespace Zetatech.Accelerate.Telemetry.Collectors;

public sealed class HttpRequestCollector : BaseMiddleware
{
    public HttpRequestCollector(RequestDelegate next) : base(next)
    {
    }

    public async override Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        var utcnow = DateTime.UtcNow;

        httpContext.Response.OnCompleted(async () =>
        {
            var duration = (DateTime.UtcNow - utcnow).TotalMilliseconds;
            var telemetryService = httpContext.RequestServices.GetRequiredService<ITelemetry>();

            await telemetryService.TrackRequestAsync($"{httpContext.Request.Method} {httpContext.Request.Path}",
                                                     httpContext.Request.GetDisplayUrl(),
                                                     "http",
                                                     httpContext.Response.StatusCode < 400,
                                                     duration,
                                                     httpContext.Connection.RemoteIpAddress,
                                                     httpContext.Response.StatusCode)
                                   .ConfigureAwait(false);
        });

        await base.InvokeAsync(httpContext)
                  .ConfigureAwait(false);
    }
}
