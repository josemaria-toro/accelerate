using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.AspNetCore.Abstractions;

namespace Zetatech.Accelerate.AspNetCore.Middlewares;

public sealed class W3CActivityMiddleware : BaseMiddleware
{
    public W3CActivityMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        var operationName = $"{httpContext.Request.Method.ToUpperInvariant()} {httpContext.Request.Path}";
        var activity = new Activity(operationName);

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
        httpContext.Response.OnCompleted(() =>
        {
            activity.Stop();

            return Task.CompletedTask;
        });
        httpContext.Response.OnStarting(() =>
        {
            httpContext.Response.Headers.TryAdd("traceparent", httpContext.TraceIdentifier);
            httpContext.Response.Headers.TryAdd("tracestate", activity.TraceStateString);

            return Task.CompletedTask;
        });

        await base.InvokeAsync(httpContext)
                  .ConfigureAwait(false);
    }
}
