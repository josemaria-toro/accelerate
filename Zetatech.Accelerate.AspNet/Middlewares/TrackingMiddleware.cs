using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zetatech.Accelerate.AspNet.Abstractions;
using Zetatech.Accelerate.Tracking;
using HttpRequest = Zetatech.Accelerate.Tracking.HttpRequest;

namespace Zetatech.Accelerate.AspNet.Middlewares;

/// <summary>
/// Middleware to track HTTP requests.
/// </summary>
public sealed class TrackingMiddleware : BaseMiddleware
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public TrackingMiddleware(RequestDelegate next) : base(next)
    {
    }

    /// <summary>
    /// Execute the logic implemented by the middleware.
    /// </summary>
    /// <param name="httpContext">
    /// The HTTP context for the current request.
    /// </param>
    public async override Task InvokeAsync(HttpContext httpContext)
    {
        var utcnow = DateTime.UtcNow;

        _ = Guid.TryParse(httpContext.TraceIdentifier, out Guid operationId);

        if (operationId == Guid.Empty && httpContext.Request.Headers.ContainsKey("x-correlation"))
        {
            _ = Guid.TryParse(httpContext.Request.Headers["x-correlation"], out operationId);
        }

        if (operationId == Guid.Empty)
        {
            operationId = Guid.NewGuid();
        }

        httpContext.TraceIdentifier = $"{operationId}";

        try
        {
            await base.InvokeAsync(httpContext);
        }
        finally
        {
            var requestBuffer = new Byte[httpContext.Request.ContentLength ?? 0];

            for (var i = 0; i < requestBuffer.Length; i++)
            {
                requestBuffer[i] = (Byte)httpContext.Request.Body.ReadByte();
            }

            var responseBuffer = new Byte[httpContext.Response.ContentLength ?? 0];

            for (var i = 0; i < responseBuffer.Length; i++)
            {
                responseBuffer[i] = (Byte)httpContext.Response.Body.ReadByte();
            }

            await httpContext.RequestServices.GetRequiredService<ITrackingService>()
                                             .TrackAsync(new HttpRequest
                                             {
                                                 Body = Encoding.UTF8.GetString(requestBuffer),
                                                 Duration = DateTime.UtcNow - utcnow,
                                                 IpAddress = httpContext.Connection.RemoteIpAddress,
                                                 Name = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                                                 OperationId = operationId,
                                                 ResponseBody = Encoding.UTF8.GetString(responseBuffer),
                                                 ResponseCode = (HttpStatusCode)httpContext.Response.StatusCode,
                                                 Success = httpContext.Response.StatusCode < 400,
                                                 Uri = new Uri(httpContext.Request.GetEncodedUrl())
                                             });
        }
    }
}
