using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Application;

namespace Zetatech.Accelerate.Web.Middlewares;

public sealed class Tracking
{
    private readonly RequestDelegate _next;

    public Tracking(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var correlationId = Guid.Empty;
        var timestamp = DateTime.UtcNow;

        if (correlationId == Guid.Empty && httpContext.Request.Headers.ContainsKey("x-correlation"))
        {
            _ = Guid.TryParse(httpContext.Request.Headers["x-correlation"], out correlationId);
        }

        if (correlationId == Guid.Empty && httpContext.Request.Headers.ContainsKey("x-correlation-id"))
        {
            _ = Guid.TryParse(httpContext.Request.Headers["x-correlation"], out correlationId);
        }

        if (correlationId == Guid.Empty)
        {
            correlationId = Guid.NewGuid();
        }

        httpContext.TraceIdentifier = $"{correlationId}";

        await _next(httpContext);

        var delta = DateTime.UtcNow - timestamp;
        var tracker = httpContext.RequestServices.GetRequiredService<ITracker>();
        var request = new Request
        {
            CorrelationId = correlationId,
            Duration = delta,
            EndPoint = httpContext.Request.GetEncodedUrl(),
            IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
            Name = $"{httpContext.Request.Method.ToUpperInvariant()} {httpContext.Request.Path}",
            StatusCode = httpContext.Response.StatusCode,
            Success = httpContext.Response.StatusCode < 400,
            Type = httpContext.Request.Protocol.ToLowerInvariant()
        };

        tracker.Track(request);
    }
}