using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        httpContext.Response.OnStarting(() =>
        {
            httpContext.Response.Headers.TryAdd("Content-Security-Policy", "frame-ancestors 'self'");
            httpContext.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
            httpContext.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
            httpContext.Response.Headers.TryAdd("X-Frame-Options", "SAMEORIGIN");

            if (httpContext.Request.IsHttps)
            {
                httpContext.Response.Headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
            }

            return Task.CompletedTask;
        });

        await _next(httpContext);
    }
}
