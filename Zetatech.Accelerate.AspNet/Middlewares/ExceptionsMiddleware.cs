using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Zetatech.Accelerate.AspNet.Abstractions;
using Zetatech.Accelerate.Exceptions;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.AspNet.Middlewares;

/// <summary>
/// Middleware to handling application exceptions.
/// </summary>
public sealed class ExceptionsMiddleware : BaseMiddleware
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public ExceptionsMiddleware(RequestDelegate next) : base(next)
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
        try
        {
            await base.InvokeAsync(httpContext);
        }
        catch (Exception ex)
        {
            if (ex is BusinessException)
            {
                httpContext.Response.StatusCode = 412;
            }
            else if (ex is ConflictException)
            {
                httpContext.Response.StatusCode = 409;
            }
            else if (ex is ForbiddenException)
            {
                httpContext.Response.StatusCode = 403;
            }
            else if (ex is NotFoundException)
            {
                httpContext.Response.StatusCode = 404;
            }
            else if (ex is UnauthorizedException)
            {
                httpContext.Response.StatusCode = 401;
            }
            else if (ex is ValidationException)
            {
                httpContext.Response.StatusCode = 400;
            }
            else
            {
                httpContext.Response.StatusCode = 500;
            }
            
            await httpContext.RequestServices.GetRequiredService<ITrackingService>()
                                             .TrackAsync(new Trace
                                             {
                                                 Exception = ex,
                                                 Message = ex.Message,
                                                 OperationId = Guid.Parse(httpContext.TraceIdentifier),
                                                 Severity = Severities.Error,
                                                 SourceTypeName = ex.Source ?? GetType().Name
                                             });
        }
    }
}
