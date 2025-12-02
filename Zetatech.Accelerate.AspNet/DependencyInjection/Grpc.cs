using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Compression;
using System.Linq;

namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds and configure grpc features into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    /// <param name="interceptors">
    /// 
    /// </param>
    public static IServiceCollection AddGrpcFeatures(this IServiceCollection serviceCollection, params Type[] interceptors)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:gRPC", false);

        if (featureEnabled)
        {
            serviceCollection.AddGrpc(options =>
            {
                options.EnableDetailedErrors = configService.GetValue<Boolean>("grpc:enableDetailedErrors", false);
                options.IgnoreUnknownServices = configService.GetValue<Boolean>("grpc:ignoreUnknownServices", true);
                options.MaxReceiveMessageSize = configService.GetValue<Int32>("grpc:maxMessageSize", 4194304);
                options.MaxSendMessageSize = configService.GetValue<Int32>("grpc:maxMessageSize", 4194304);
                options.ResponseCompressionAlgorithm = configService.GetValue<String>("grpc:compressionAlgorithm", "gzip");
                options.ResponseCompressionLevel = configService.GetValue<CompressionLevel>("grpc:compressionLevel", CompressionLevel.Optimal);

                if (interceptors != null && interceptors.Any())
                {
                    foreach (var interceptor in interceptors)
                    {
                        options.Interceptors.Add(interceptor);
                    }
                }
            });
        }

        return serviceCollection;
    }
}
