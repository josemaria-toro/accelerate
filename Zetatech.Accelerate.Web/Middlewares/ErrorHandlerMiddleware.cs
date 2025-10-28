using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Web.Middlewares;

/// <summary>
/// Represents an implementation for a custom middleware to handle errors in the application.
/// </summary>
public class ErrorHandlerMiddleware : BaseMiddleware
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public ErrorHandlerMiddleware(RequestDelegate next) : base(next)
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
        try
        {
            await base.InvokeAsync(context);
        }
        catch (Exception ex)
        {
            var statusCode = 500;

            if (ex is BusinessException)
            {
                statusCode = 412;
            }
            else if (ex is ConflictException)
            {
                statusCode = 409;
            }
            else if (ex is ForbiddenException)
            {
                statusCode = 403;
            }
            else if (ex is NotFoundException)
            {
                statusCode = 404;
            }
            else if (ex is UnauthorizedException)
            {
                statusCode = 401;
            }
            else if (ex is ValidationException)
            {
                statusCode = 400;
            }

            context.Response.StatusCode = statusCode;

            var operationId = Guid.Parse(context.TraceIdentifier);
            var requestUrl = context.Request.GetEncodedUrl();
            var loggingService = context.RequestServices.GetRequiredService<ILoggerFactory>()
                                                        .CreateLogger<ErrorHandlerMiddleware>();

            loggingService.LogCritical(ex, $"{operationId} - Error handling HTTP request '{requestUrl}' ({statusCode})");
        }
    }
}
