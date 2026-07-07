using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Caching;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICache, InMemoryCache>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var inMemoryCacheOptions = new InMemoryCacheOptions
            {
                MaxItems = configService.GetValue<Int32>("caching:inMemory:maxItems", 4096)
            };

            return new InMemoryCache(Options.Create(inMemoryCacheOptions), loggerFactory);
        });

        return serviceCollection;
    }
}