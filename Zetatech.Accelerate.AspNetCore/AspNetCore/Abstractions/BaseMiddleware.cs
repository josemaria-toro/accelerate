using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.AspNetCore.Abstractions;

public abstract class BaseMiddleware
{
    private readonly RequestDelegate _next;

    protected BaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public virtual async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        await _next(httpContext).ConfigureAwait(false);
    }
}
