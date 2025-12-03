using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Caching.Services;

namespace Zetatech.Accelerate.Caching.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds the caching service into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddCachingService(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:caching", false);

        if (featureEnabled)
        {
            serviceCollection.AddTransient<ICachingService>(serviceProvider =>
            {
                var configService = serviceProvider.GetRequiredService<IConfiguration>();
                var options = Options.Create(new MemoryCachingServiceOptions
                {
                    DefaultExpirationTime = configService.GetValue<Int32>("caching:defaultExpirationTime", 86400),
                    MaxSize = configService.GetValue<Int32>("caching:maxSize", 1000),
                });

                return new MemoryCachingService(options);
            });
        }

        return serviceCollection;
    }
}
