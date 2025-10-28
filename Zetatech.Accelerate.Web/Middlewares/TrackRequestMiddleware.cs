using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using Zetatech.Accelerate.Telemetry;

namespace Zetatech.Accelerate.Web.Middlewares;

/// <summary>
/// Represents an implementation for a custom middleware to track http requests in the telemetry service.
/// </summary>
public class TrackRequestMiddleware : BaseMiddleware
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public TrackRequestMiddleware(RequestDelegate next) : base(next)
    {
    }

    /// <summary>
    /// Execute the logic implemented by the middleware.
    /// </summary>
    /// <param name="context">
    /// The HTTP context for the current request.
    /// </param>
    public async override Task InvokeAsync(HttpContext context)
    {
        var timestamp = DateTime.UtcNow;

        try
        {
            await base.InvokeAsync(context);
        }
        finally
        {
            var operationId = Guid.Parse(context.TraceIdentifier);
            var telemetryService = context.RequestServices.GetRequiredService<ITelemetryService>();

            telemetryService.Track(new Request
            {
                Duration = DateTime.UtcNow - timestamp,
                IpAddress = context.Connection.RemoteIpAddress,
                Name = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.Path}",
                OperationId = operationId,
                ResponseCode = (HttpStatusCode)context.Response.StatusCode,
                Success = context.Response.StatusCode < 400,
                Timestamp = timestamp,
                Uri = new Uri(context.Request.GetDisplayUrl())
            });
        }
    }
}
