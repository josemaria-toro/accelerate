using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.AspNetCore.Abstractions;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.AspNetCore.Middlewares;

public sealed class RequiredHeaderMiddleware : BaseMiddleware
{
    private readonly String _headerName;
    private readonly Object _headerValue;

    public RequiredHeaderMiddleware(RequestDelegate next, String headerName, Object headerValue = null) : base(next)
    {
        if (String.IsNullOrEmpty(headerName))
        {
            throw new ArgumentException("The provided header name is invalid", nameof(headerName));
        }

        _headerName = headerName;
        _headerValue = headerValue;
    }

    public async override Task InvokeAsync(HttpContext httpContext)
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

        await base.InvokeAsync(httpContext)
                  .ConfigureAwait(false);
    }
}
