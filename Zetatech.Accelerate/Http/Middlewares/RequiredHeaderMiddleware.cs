using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Http.Middlewares;

public sealed class RequiredHeaderMiddleware
{
    private readonly String _headerName;
    private readonly Object _headerValue;
    private readonly RequestDelegate _next;

    public RequiredHeaderMiddleware(RequestDelegate next,
                                    String headerName,
                                    Object headerValue = null)
    {
        if (String.IsNullOrEmpty(headerName))
        {
            throw new ArgumentException("The provided header name is invalid", nameof(headerName));
        }

        _headerName = headerName;
        _headerValue = headerValue;
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

        if (_headerValue != null && httpContext.Request.Headers[_headerName] != _headerValue)
        {
            throw new ValidationException($"The value of header '{_headerName}' doesn't match with the expected value");
        }

        await _next(httpContext);
    }
}
