using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Telemetry;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDeepSightTelemetry(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITelemetry, DeepSightTelemetry>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var loggingFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var telemetryOptions = new DeepSightTelemetryOptions
            {
                Tenant = configService.GetValue<Guid>("telemetry:deepSight:tenant"),
                Uri = configService.GetValue<Uri>("telemetry:deepSight:url")
            };

            return new DeepSightTelemetry(Options.Create(telemetryOptions), loggingFactory);
        });

        return serviceCollection;
    }
}