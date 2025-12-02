using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.AspNet.Abstractions;

/// <summary>
/// Represents a base class for implementing custom middlewares in ASP.NET Core MVC.
/// </summary>
public abstract class BaseMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    protected BaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Execute the logic implemented by the middleware.
    /// </summary>
    /// <param name="context">
    /// The HTTP context for the current request.
    /// </param>
    public async virtual Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}
