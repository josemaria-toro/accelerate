using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Cache;
using Zetatech.Accelerate.Jobs;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<InMemoryCacheOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.MaxItems = configService.GetValue<Int32>("inMemoryCache:maxItems", 1000);
                         });

        serviceCollection.AddHostedService<InMemoryCacheCleanerJob>();
        serviceCollection.AddSingleton<ICache, InMemoryCache>();

        return serviceCollection;
    }
}
