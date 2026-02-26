using System;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static class RateLimits
{
    public static IServiceCollection AddRateLimitsPolicies(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("appSettings:rateLimits", false);

        if (featureEnabled)
        {
            serviceCollection.AddRateLimiter(options =>
            {
                options.AddConcurrencyLimiter("default", limiterOptions =>
                {
                    limiterOptions.PermitLimit = configService.GetValue<Int32>("rateLimits:maxRequests", 25);
                    limiterOptions.QueueLimit = configService.GetValue<Int32>("rateLimits:queueSize", 1000);
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.RejectionStatusCode = 429;
            });
        }

        return serviceCollection;
    }
    public static WebApplication UseRateLimitsFeatures(this WebApplication webApplication)
    {
        var featureEnabled = webApplication.Configuration.GetValue<Boolean>("appSettings:rateLimits", false);

        if (featureEnabled)
        {
            webApplication.UseRateLimiter();
        }

        return webApplication;
    }
}