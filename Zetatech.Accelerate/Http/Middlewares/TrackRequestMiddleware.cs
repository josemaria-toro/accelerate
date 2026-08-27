using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Telemetry;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class TrackRequestMiddleware
{
    private readonly RequestDelegate _next;

    public TrackRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
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

        await _next(httpContext);
    }
}
