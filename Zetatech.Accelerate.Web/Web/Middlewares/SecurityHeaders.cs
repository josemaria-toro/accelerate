using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.Web.Middlewares;

public sealed class SecurityHeaders
{
    private readonly RequestDelegate _next;

    public SecurityHeaders(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        httpContext.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
        httpContext.Response.Headers["X-Content-Type-Options"] = "nosniff";
        httpContext.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        httpContext.Response.Headers["X-XSS-Protection"] = "1; mode=block";

        await _next(httpContext);
    }
}