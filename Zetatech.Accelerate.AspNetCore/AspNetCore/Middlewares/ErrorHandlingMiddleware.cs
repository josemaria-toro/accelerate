using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.AspNetCore.Abstractions;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.AspNetCore.Middlewares;

public sealed class ErrorHandlingMiddleware : BaseMiddleware
{
    public ErrorHandlingMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        try
        {
            await base.InvokeAsync(httpContext)
                      .ConfigureAwait(false);
        }
        catch (ConfigurationException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
        catch (ConflictException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status409Conflict);
        }
        catch (DependencyException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status424FailedDependency);
        }
        catch (DomainException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status412PreconditionFailed);
        }
        catch (ForbiddenException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status403Forbidden);
        }
        catch (NotFoundException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status404NotFound);
        }
        catch (UnauthorizedException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status401Unauthorized);
        }
        catch (UnavailableException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status502BadGateway);
        }
        catch (ValidationException ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            HandleException(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
    }
    private void HandleException(HttpContext httpContext, Exception exception, Int32 statusCode)
    {
        var errorMessage = $"An error of type '{exception.GetType()}' was raised: {exception.Message}";

        httpContext.RequestServices.GetService<ILoggerFactory>()?
                                   .CreateLogger(GetType().Name)?
                                   .LogError(exception, errorMessage);

        httpContext.Response.StatusCode = statusCode;
    }
}
