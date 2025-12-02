using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.RateLimiting;

namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds and configure rate limits policies into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddRateLimitsPolicies(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:rateLimits", false);

        if (featureEnabled)
        {
            serviceCollection.AddRateLimiter(options =>
            {
                options.AddConcurrencyLimiter("default", limiterOptions =>
                {
                    limiterOptions.PermitLimit = configService.GetValue<Int32>("rateLimits:maxRequests", 25);
                    limiterOptions.QueueLimit = configService.GetValue<Int32>("rateLimits:queueSize", 10);
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.RejectionStatusCode = 429;
            });
        }

        return serviceCollection;
    }
    /// <summary>
    /// Configure the web application to use rate limits features.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseRateLimitsFeatures(this WebApplication webApplication)
    {
        var featureEnabled = webApplication.Configuration.GetValue<Boolean>("features:rateLimits", false);

        if (featureEnabled)
        {
            webApplication.UseRateLimiter();
        }

        return webApplication;
    }
}
