using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Caching;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ICache, InMemoryCache>();
    }
    public static IServiceCollection AddInMemoryCacheOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<InMemoryCacheOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.MaxItems = configService.GetValue<Int32>("caching:inMemory:maxItems", 1000);
                         });

        return serviceCollection;
    }
}