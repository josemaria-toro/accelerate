using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.AspNetCore.Abstractions;

public abstract class BaseMiddleware
{
    private readonly RequestDelegate _next;

    protected BaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    protected async Task ExecuteNextAsync(HttpContext httpContext)
    {
        await _next(httpContext);
    }
    public abstract Task InvokeAsync(HttpContext httpContext);
}
