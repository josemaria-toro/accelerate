using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Logging;
using Zetatech.Accelerate.Logging.Providers;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDeepSightLoggerProvider(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ILoggerProvider, DeepSightLoggerProvider>();
    }
    public static IServiceCollection AddDeepSightLoggerProviderOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<DeepSightLoggerOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.AppName = configService.GetValue<String>("logging:deepSight:appName", String.Empty);
                             options.AppVersion = configService.GetValue<Version>("logging:deepSight:appVersion", Version.Parse("1.0.0"));
                             options.LogLevel = configService.GetValue<LogLevel>("logging:deepSight:logLevel", LogLevel.Warning);
                             options.Tenant = configService.GetValue<Guid>("logging:deepSight:tenant");
                             options.Uri = configService.GetValue<Uri>("logging:deepSight:url");
                         });

        return serviceCollection;
    }
}