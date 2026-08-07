using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("cors:enabled", false);

        if (featureEnabled)
        {
            serviceCollection.AddCors(options =>
            {
                var corsSection = configService.GetSection("cors:policies");
                var corsPolicies = corsSection.GetChildren();

                foreach (var corsPolicy in corsPolicies)
                {
                    options.AddPolicy(corsPolicy.Key, policy =>
                    {
                        var allowedHeaders = corsPolicy.GetValue<String>("headers", "*");

                        if (allowedHeaders == "*")
                        {
                            policy.AllowAnyHeader();
                        }
                        else
                        {
                            policy.WithHeaders(allowedHeaders.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                        }

                        var allowedMethods = corsPolicy.GetValue<String>("methods", "*");

                        if (allowedMethods == "*")
                        {
                            policy.AllowAnyMethod();
                        }
                        else
                        {
                            policy.WithMethods(allowedMethods.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                        }

                        var allowedOrigins = corsPolicy.GetValue<String>("origins", "*");

                        if (allowedOrigins == "*")
                        {
                            policy.AllowAnyOrigin();
                        }
                        else
                        {
                            policy.WithOrigins(allowedOrigins.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                        }
                    });
                }
            });
        }

        return serviceCollection;
    }
    public static IApplicationBuilder UseCorsFeatures(this IApplicationBuilder applicationBuilder)
    {
        var configService = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("cors:enabled", false);

        if (featureEnabled)
        {
            var corsSection = configService.GetSection("cors:policies");
            var corsPolicies = corsSection.GetChildren();

            foreach (var corsPolicy in corsPolicies)
            {
                applicationBuilder.UseCors(corsPolicy.Key);
            }
        }

        return applicationBuilder;
    }
}
