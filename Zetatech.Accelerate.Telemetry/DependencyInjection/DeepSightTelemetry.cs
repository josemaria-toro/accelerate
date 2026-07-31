using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Telemetry;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDeepSightTelemetry(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ITelemetry, DeepSightTelemetry>();
    }
    public static IServiceCollection AddDeepSightTelemetryOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<DeepSightTelemetryOptions>()
                         .Configure<IConfiguration>((options, configService) =>
                         {
                             options.AppName = configService.GetValue<String>("telemetry:deepSight:appName", String.Empty);
                             options.AppVersion = configService.GetValue<Version>("telemetry:deepSight:appVersion", Version.Parse("1.0.0"));
                             options.Tenant = configService.GetValue<Guid>("telemetry:deepSight:tenant");
                             options.Uri = configService.GetValue<Uri>("telemetry:deepSight:url");
                         });

        return serviceCollection;
    }
}