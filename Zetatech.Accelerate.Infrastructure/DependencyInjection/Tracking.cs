using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Infrastructure.Tracking;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Infrastructure.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds the configuration service into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddTrackingService(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:tracking", false);

        if (featureEnabled)
        {
            serviceCollection.AddTransient<ITrackingService>(serviceProvider =>
            {
                var configService = serviceProvider.GetRequiredService<IConfiguration>();
                var options = Options.Create(new TrackingServiceOptions
                {
                    AppId = configService.GetValue<Guid>("appSettings:appId", Guid.Empty),
                    ConnectionString = configService.GetConnectionString("messageBroker")
                });

                return new TrackingService(options);
            });
        }

        return serviceCollection;
    }
}
