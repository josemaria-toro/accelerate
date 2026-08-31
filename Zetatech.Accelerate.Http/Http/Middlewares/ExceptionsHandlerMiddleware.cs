using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class ExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        try
        {
            await _next(httpContext);
        }
        catch (ConfigurationException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
        catch (ConflictException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status409Conflict);
        }
        catch (DependencyException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status424FailedDependency);
        }
        catch (DomainException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status412PreconditionFailed);
        }
        catch (ForbiddenException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status403Forbidden);
        }
        catch (NotFoundException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status404NotFound);
        }
        catch (UnauthorizedException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status401Unauthorized);
        }
        catch (UnavailableException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status502BadGateway);
        }
        catch (ValidationException ex)
        {
            Handle(httpContext, ex, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            Handle(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
    }
    private static void Handle(HttpContext httpContext,
                               Exception exception,
                               Int32 statusCode)
    {
        var errorMessage = $"An error of type '{exception.GetType()}' was raised: {exception.Message}";

        httpContext.RequestServices.GetService<ILoggerFactory>()?
                                   .CreateLogger<ExceptionsHandlerMiddleware>()?
                                   .LogError(exception, errorMessage);

        httpContext.Response.StatusCode = statusCode;
    }
}
