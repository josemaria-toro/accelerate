using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Telemetry.Collectors;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddCpuCollector(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddHostedService<CpuCollector>();
    }
    public static IServiceCollection AddRamCollector(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddHostedService<RamCollector>();
    }
    public static IServiceCollection AddTelemetryCollectors(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddCpuCollector()
                                .AddRamCollector();
    }
    public static IApplicationBuilder UseTelemetrysCollectors(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<HttpRequestCollector>();
    }
}
