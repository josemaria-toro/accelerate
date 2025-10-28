using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Telemetry;
using Zetatech.Accelerate.Telemetry.Services;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Telemetry
{
    public static IServiceCollection AddDatabaseTelemetry(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITelemetryService>(serviceProvider =>
        {
            var configService = serviceProvider.GetRequiredService<IConfiguration>();
            var telemetryServiceOptions = new PostgreSqlTelemetryServiceOptions
            {
                AppId = configService.GetValue<Guid>("appSettings:appId", Guid.Empty),
                ConnectionString = configService.GetValue<String>("connectionStrings:database", String.Empty)
            };

            return new PostgreSqlTelemetryService(Options.Create(telemetryServiceOptions));
        });

        return serviceCollection;
    }
}