using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Logging.Factories;
using Zetatech.Accelerate.Logging.Providers;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Logging
{
    public static IServiceCollection AddConsoleLogging(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILoggerProvider>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggingProviderOptions = new ConsoleLoggingProviderOptions
            {
                AppId = configService.GetValue<Guid>("appSettings:appId", Guid.Empty),
                Critical = configService.GetValue<Boolean>("logging:console:critical", true),
                Debug = configService.GetValue<Boolean>("logging:console:debug", false),
                Error = configService.GetValue<Boolean>("logging:console:error", true),
                Information = configService.GetValue<Boolean>("logging:console:information", true),
                Warning = configService.GetValue<Boolean>("logging:console:warning", true)
            };

            return new ConsoleLoggingProvider(Options.Create(loggingProviderOptions));
        });

        return serviceCollection;
    }
    public static IServiceCollection AddDatabaseLogging(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILoggerProvider>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggingProviderOptions = new PostgreSqlLoggingProviderOptions
            {
                AppId = configService.GetValue<Guid>("logging:appId", Guid.Empty),
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty),
                Critical = configService.GetValue<Boolean>("logging:database:critical", true),
                Debug = configService.GetValue<Boolean>("logging:database:debug", false),
                Error = configService.GetValue<Boolean>("logging:database:error", true),
                Information = configService.GetValue<Boolean>("logging:database:information", true),
                Warning = configService.GetValue<Boolean>("logging:database:warning", true)
            };

            return new PostgreSqlLoggingProvider(Options.Create(loggingProviderOptions));
        });

        return serviceCollection;
    }
    public static IServiceCollection AddLoggingFactory(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILoggerFactory>(serviceProvider =>
        {
            var loggingFactoryOptions = new LoggingFactoryOptions();
            var loggingFactory = new LoggingFactory(Options.Create(loggingFactoryOptions));
            var loggingProviders = serviceProvider.GetServices<ILoggerProvider>();

            foreach (var loggingProvider in loggingProviders)
            {
                loggingFactory.AddProvider(loggingProvider);
            }

            return loggingFactory;
        });

        return serviceCollection;
    }
}