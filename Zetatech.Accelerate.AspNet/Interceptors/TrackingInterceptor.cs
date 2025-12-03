using Grpc.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using Zetatech.Accelerate.AspNet.Abstractions;
using Zetatech.Accelerate.Tracking;
using HttpRequest = Zetatech.Accelerate.Tracking.HttpRequest;

namespace Zetatech.Accelerate.AspNet.Interceptors;

/// <summary>
/// Interceptor to track gRPC requests.
/// </summary>
public sealed class TrackingInterceptor : BaseInterceptor
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public TrackingInterceptor() : base()
    {
    }

    /// <summary>
    /// Server-side handler for intercepting and incoming unary call.
    /// </summary>
    /// <typeparam name="TRequest">
    /// Request message type for this method.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Response message type for this method.
    /// </typeparam>
    /// <param name="gRPCRequest">
    /// The request value of the incoming invocation.
    /// </param>
    /// <param name="serverCallContext">
    /// The context of the invocation.
    /// </param>
    /// <param name="continuation">
    /// A delegate that asynchronously proceeds with the invocation, calling the next interceptor in the chain, or the service request handler, in case of the last interceptor and return the response value of the RPC. The interceptor can choose to call it zero or more times at its discretion.
    /// </param>
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest gRPCRequest, ServerCallContext serverCallContext, UnaryServerMethod<TRequest, TResponse> continuation) where TRequest : class
                                                                                                                                                                                                  where TResponse : class
    {
        TResponse gRPCResponse = null;
        var httpContext = serverCallContext.GetHttpContext();
        var utcnow = DateTime.UtcNow;

        _ = Guid.TryParse(httpContext.TraceIdentifier, out Guid operationId);

        if (operationId == Guid.Empty)
        {
            var correlationId = serverCallContext.RequestHeaders.Get("x-correlation");
            _ = Guid.TryParse(correlationId?.Value, out operationId);
        }

        if (operationId == Guid.Empty)
        {
            operationId = Guid.NewGuid();
        }

        httpContext.TraceIdentifier = $"{operationId}";

        try
        {
            gRPCResponse = await continuation(gRPCRequest, serverCallContext);
        }
        finally
        {
            var httpStatusCode = serverCallContext.Status.StatusCode switch
            {
                StatusCode.AlreadyExists => HttpStatusCode.Conflict,
                StatusCode.DeadlineExceeded => HttpStatusCode.RequestTimeout,
                StatusCode.FailedPrecondition => HttpStatusCode.PreconditionFailed,
                StatusCode.InvalidArgument => HttpStatusCode.BadRequest,
                StatusCode.NotFound => HttpStatusCode.NotFound,
                StatusCode.OK => HttpStatusCode.OK,
                StatusCode.OutOfRange => HttpStatusCode.BadRequest,
                StatusCode.PermissionDenied => HttpStatusCode.Forbidden,
                StatusCode.ResourceExhausted => HttpStatusCode.TooManyRequests,
                StatusCode.Unauthenticated => HttpStatusCode.Unauthorized,
                StatusCode.Unimplemented => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError
            };

            await httpContext.RequestServices.GetRequiredService<ITrackingService>()
                                             .TrackAsync(new HttpRequest
                                             {
                                                 Body = $"{gRPCRequest}",
                                                 Duration = DateTime.UtcNow - utcnow,
                                                 IpAddress = IPAddress.Parse(serverCallContext.Peer),
                                                 Name = serverCallContext.Method,
                                                 OperationId = operationId,
                                                 ResponseBody = $"{gRPCResponse}",
                                                 ResponseCode = httpStatusCode,
                                                 Success = (Int32)httpStatusCode < 400,
                                                 Uri = new Uri(httpContext.Request.GetEncodedUrl())
                                             });
        }

        return gRPCResponse;
    }
}
