using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging.Console;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddConsoleLoggerProvider(this IServiceCollection serviceCollection)
    {
        var boundedChannel = Channel.CreateBounded<ConsoleLoggerEntry>(new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true
        });

        serviceCollection.AddSingleton(boundedChannel)
                         .AddSingleton<ILoggerProvider, ConsoleLoggerProvider>();

        return serviceCollection.AddHostedService<ConsoleLoggerJob>();
    }
    public static IServiceCollection AddConsoleLoggerProviderOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<ConsoleLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.LogLevel = configService.GetValue<LogLevel>("logging:logLevel:console", LogLevel.Warning);
                         });

        return serviceCollection;
    }
}
