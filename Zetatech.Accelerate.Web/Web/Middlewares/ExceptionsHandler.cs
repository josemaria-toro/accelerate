using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Web.Middlewares;

public sealed class ExceptionsHandler
{
    private readonly RequestDelegate _next;

    public ExceptionsHandler(RequestDelegate next)
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
        catch (BusinessException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status412PreconditionFailed;
            WriteException(ex);
        }
        catch (ConfigurationException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            WriteException(ex);
        }
        catch (ConflictException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            WriteException(ex);
        }
        catch (DependencyException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            WriteException(ex);
        }
        catch (ForbiddenException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            WriteException(ex);
        }
        catch (NotFoundException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            WriteException(ex);
        }
        catch (UnauthorizedException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            WriteException(ex);
        }
        catch (UnavailableException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            WriteException(ex);
        }
        catch (ValidationException ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            WriteException(ex);
        }
        catch (Exception ex)
        {
            Clear(httpContext);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            WriteException(ex);
        }
    }
    private static void WriteException(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"An error of type '{ex.GetType()}' was raised: {ex.Message} {ex.StackTrace}");
        Console.ResetColor();
    }
}