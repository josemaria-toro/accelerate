using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging;
using Zetatech.Accelerate.Logging.Jobs;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddConsoleLoggerProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<ConsoleLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.LogLevel = configService.GetValue<LogLevel>("logging:logLevel:console", LogLevel.Debug);
                         });

        serviceCollection.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>();
        serviceCollection.AddHostedService<ConsoleWriterJob>();

        return serviceCollection;
    }
}
