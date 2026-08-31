using System;
using System.IO;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging.FlatFile;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddFlatFileLoggerProvider(this IServiceCollection serviceCollection)
    {
        var boundedChannel = Channel.CreateBounded<FlatFileLoggerEntry>(new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true
        });

        serviceCollection.AddSingleton(boundedChannel)
                         .AddSingleton<ILoggerProvider, FlatFileLoggerProvider>();

        return serviceCollection.AddHostedService<FlatFileLoggerJob>();
    }
    public static IServiceCollection AddFlatFileLoggerProviderOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<FlatFileLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.FileName = configService.GetValue<String>("logging:flatFile:fileName", $"{DateTime.UtcNow:yyyyMMdd}");
                             options.LogLevel = configService.GetValue<LogLevel>("logging:logLevel:flatFile", LogLevel.Warning);
                             options.MaxSize = configService.GetValue<Int64>("logging:flatFile:maxSize", 104857600);
                             options.Path = configService.GetValue<String>("logging:flatFile:path", Path.Combine(Environment.CurrentDirectory, "logs"));
                         });

        return serviceCollection;
    }
}
