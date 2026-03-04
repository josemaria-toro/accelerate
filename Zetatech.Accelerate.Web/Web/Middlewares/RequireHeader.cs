using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Exceptions;

namespace Zetatech.Accelerate.Web.Middlewares;

public sealed class RequireHeader
{
    private readonly String _headerName;
    private readonly RequestDelegate _next;

    public RequireHeader(RequestDelegate next, String headerName)
    {
        _headerName = headerName;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey(_headerName))
        {
            throw new ValidationException($"The header '{_headerName}' is missing");
        }

        await _next(httpContext);
    }
}