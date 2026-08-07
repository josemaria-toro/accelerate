using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class W3CActivityMiddleware
{
    private readonly RequestDelegate _next;

    public W3CActivityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        var activity = new Activity($"{httpContext.Request.Method.ToUpperInvariant()} {httpContext.Request.Path}");

        activity.SetIdFormat(ActivityIdFormat.W3C);

        httpContext.Request.Headers.TryGetValue("traceparent", out var traceParent);
        httpContext.Request.Headers.TryGetValue("tracestate", out var traceState);

        if (ActivityContext.TryParse(traceParent, traceState, out var parentContext))
        {
            activity.SetParentId(parentContext.TraceId, parentContext.SpanId, parentContext.TraceFlags);

            if (!String.IsNullOrEmpty(traceState))
            {
                activity.TraceStateString = traceState;
            }
        }

        activity.Start();

        httpContext.TraceIdentifier = activity.Id;
        httpContext.Response.OnStarting(() =>
        {
            httpContext.Response.Headers.TryAdd("traceparent", httpContext.TraceIdentifier);
            httpContext.Response.Headers.TryAdd("tracestate", activity.TraceStateString);

            return Task.CompletedTask;
        });

        try
        {
            await _next(httpContext);
        }
        finally
        {
            activity.Stop();
        }
    }
}