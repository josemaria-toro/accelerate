using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging.Console;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddConsoleLoggerProvider(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>();
    }
    public static IServiceCollection AddConsoleLoggerProviderOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<ConsoleLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.LogLevel = configService.GetValue<LogLevel>("logging:console:logLevel", LogLevel.Warning);
                         });

        return serviceCollection;
    }
}