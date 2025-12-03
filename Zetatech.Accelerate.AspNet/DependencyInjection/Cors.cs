using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds and configure cors policies into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddCorsPolicies(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:cors", false);

        if (featureEnabled)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                var configSection = configService.GetSection("cors");
                var configPolicies = configSection.GetChildren();

                foreach(var configPolicy in configPolicies)
                {
                    options.AddPolicy(configPolicy.Key, policy =>
                    {
                        var headers = configPolicy.GetValue<String>("headers", "*");

                        if (String.IsNullOrEmpty(headers) || headers == "*")
                        {
                            policy.AllowAnyHeader();
                        }
                        else
                        {
                            policy.WithHeaders(headers.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                        }

                        var methods = configPolicy.GetValue<String>("methods", "*");

                        if (String.IsNullOrEmpty(methods) || methods == "*")
                        {
                            policy.AllowAnyMethod();
                        }
                        else
                        {
                            policy.WithMethods(methods.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                        }

                        var origin = configPolicy.GetValue<String>("origin", "*");

                        if (String.IsNullOrEmpty(origin) || origin == "*")
                        {
                            policy.AllowAnyOrigin();
                        }
                        else
                        {
                            policy.WithOrigins(origin);
                        }
                    });
                }
            });
        }

        return serviceCollection;
    }
    /// <summary>
    /// Configure the web application to use cors features.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseCorsFeatures(this WebApplication webApplication)
    {
        var corsEnabled = webApplication.Configuration.GetValue<Boolean>("features:cors", false);

        if (corsEnabled)
        {
            webApplication.UseCors();
        }

        return webApplication;
    }
}
