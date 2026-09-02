using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging;
using Zetatech.Accelerate.Logging.Jobs;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddFlatFileLoggerProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<FlatFileLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.FileName = configService.GetValue<String>("logging:flatFile:fileName", $"{DateTime.UtcNow:yyyyMMdd}");
                             options.LogLevel = configService.GetValue<LogLevel>("logging:logLevel:flatFile", LogLevel.Debug);
                             options.MaxSize = configService.GetValue<Int64>("logging:flatFile:maxSize", 104857600);
                             options.Path = configService.GetValue<String>("logging:flatFile:path", Path.Combine(Environment.CurrentDirectory, "logs"));
                         });

        serviceCollection.AddSingleton<ILoggerProvider, FlatFileLoggerProvider>();
        serviceCollection.AddHostedService<FlatFileWriterJob>();

        return serviceCollection;
    }
}
