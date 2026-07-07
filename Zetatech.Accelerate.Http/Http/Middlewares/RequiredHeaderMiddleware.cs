using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class RequiredHeaderMiddleware
{
    private readonly String _headerName;
    private readonly RequestDelegate _next;

    public RequiredHeaderMiddleware(RequestDelegate next, String headerName)
    {
        if (String.IsNullOrEmpty(headerName))
        {
            throw new ArgumentException("The provided header name is invalid", nameof(headerName));
        }

        _headerName = headerName;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentException("The provided http context must be a valid instance", nameof(httpContext));
        }

        if (!httpContext.Request.Headers.ContainsKey(_headerName))
        {
            throw new ValidationException($"The header '{_headerName}' is required but it's missing");
        }

        await _next(httpContext);
    }
}