using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging;
using Zetatech.Accelerate.Logging.Providers;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static ILoggingBuilder AddDeepSightLoggerProvider(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.Services.AddDeepSightLoggerProvider();

        return loggingBuilder;
    }
    public static IServiceCollection AddDeepSightLoggerProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILoggerProvider, DeepSightLoggerProvider>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggerOptions = new DeepSightLoggerOptions
            {
                AppName = configService.GetValue<String>("logging:deepSight:appName", String.Empty),
                AppVersion = configService.GetValue<Version>("logging:deepSight:appVersion", Version.Parse("1.0.0")),
                LogLevel = configService.GetValue<LogLevel>("logging:deepSight:logLevel", LogLevel.Information),
                Tenant = configService.GetValue<Guid>("logging:deepSight:tenant"),
                Uri = configService.GetValue<Uri>("logging:deepSight:url")
            };

            return new DeepSightLoggerProvider(Options.Create(loggerOptions));
        });

        return serviceCollection;
    }
}