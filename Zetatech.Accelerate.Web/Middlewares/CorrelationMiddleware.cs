using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Web.Middlewares;

/// <summary>
/// Represents an implementation for a custom middleware to initialize the correlation data in the http context.
/// </summary>
public class CorrelationMiddleware : BaseMiddleware
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public CorrelationMiddleware(RequestDelegate next) : base(next)
    {
    }

    /// <summary>
    /// Execute the logic implemented by the middleware.
    /// </summary>
    /// <param name="context">
    /// The HTTP context for the current request.
    /// </param>
    public override async Task InvokeAsync(HttpContext context)
    {
        var allowedHeaders = new String[] { "x-correlation-id", "x-operation-id", "x-request-id" };
        var allowedParameters = new String[] { "state" };
        var operationId = Guid.Empty;

        foreach (var allowedHeader in allowedHeaders)
        {
            if (context.Request.Headers.TryGetValue(allowedHeader, out var operationIdValue))
            {
                if (Guid.TryParse(operationIdValue, out operationId))
                {
                    break;
                }
            }
        }

        if (operationId == Guid.Empty)
        {
            foreach (var allowedParameter in allowedParameters)
            {
                if (context.Request.Query.TryGetValue(allowedParameter, out var operationIdValue))
                {
                    if (Guid.TryParse(operationIdValue, out operationId))
                    {
                        break;
                    }
                }
            }
        }

        if (operationId == Guid.Empty)
        {
            operationId = Guid.NewGuid();
        }

        context.TraceIdentifier = $"{operationId}";

        await base.InvokeAsync(context);
    }
}
