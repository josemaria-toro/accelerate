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
    public static ILoggingBuilder AddConsoleLoggerProvider(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.Services.AddConsoleLoggerProvider();

        return loggingBuilder;
    }
    public static IServiceCollection AddConsoleLoggerProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggerOptions = new ConsoleLoggerOptions
            {
                LogLevel = configService.GetValue<LogLevel>("logging:console:logLevel", LogLevel.Information)
            };

            return new ConsoleLoggerProvider(Options.Create(loggerOptions));
        });

        return serviceCollection;
    }
}