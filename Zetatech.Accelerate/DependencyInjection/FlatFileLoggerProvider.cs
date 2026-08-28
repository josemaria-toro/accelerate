using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging.FlatFile;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddFlatFileLoggerProvider(this IServiceCollection serviceCollection)
    {
        builder.Services.AddHostedService(sp => new FileLoggerProcessor(logChannel, logFilePath));
        return serviceCollection.AddSingleton<ILoggerProvider, FlatFileLoggerProvider>();
    }
    public static IServiceCollection AddFlatFileLoggerProviderOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<FlatFileLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.LogLevel = configService.GetValue<LogLevel>("logging:logLevel:flatFile", LogLevel.Warning);
                         });

        return serviceCollection;
    }
}