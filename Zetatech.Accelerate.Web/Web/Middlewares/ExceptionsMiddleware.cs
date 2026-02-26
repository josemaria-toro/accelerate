using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Web.Middlewares;

public sealed class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private static void Clear(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";

        if (httpContext.Response.Body.CanSeek)
        {
            httpContext.Response.Body.SetLength(0);
        }

        httpContext.Response.Headers.Clear();
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BusinessException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status412PreconditionFailed;
        }
        catch (ConfigurationException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        catch (ConflictException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        }
        catch (DependencyException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
        }
        catch (ForbiddenException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        catch (NotFoundException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (UnauthorizedException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        catch (UnavailableException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
        }
        catch (ValidationException)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}