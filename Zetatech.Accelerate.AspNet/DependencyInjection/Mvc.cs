using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Zetatech.Accelerate.AspNet.DependencyInjection;

/// <summary>
/// Extension methods to configure the dependency injection.
/// </summary>
public static partial class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds and configure the mvc features into the service collection descriptors.
    /// </summary>
    /// <param name="serviceCollection">
    /// Collection of service descriptors.
    /// </param>
    public static IServiceCollection AddMvcFeatures(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configService = serviceProvider.GetRequiredService<IConfiguration>();
        var featureEnabled = configService.GetValue<Boolean>("features:mvc", false);

        if (featureEnabled)
        {
            var mvcBuilder = serviceCollection.AddMvc();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                    .SelectMany(x => x.ExportedTypes)
                                                    .Where(x => x.IsClass && !x.IsAbstract && x.BaseType != null)
                                                    .Where(x => x.IsAssignableFrom(typeof(ControllerBase)) ||
                                                                x.IsInstanceOfType(typeof(ControllerBase)) ||
                                                                x.IsSubclassOf(typeof(ControllerBase)))
                                                    .Select(x => x.Assembly)
                                                    .Distinct();

            foreach (var assembly in assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
            }

            mvcBuilder.AddControllersAsServices();
            mvcBuilder.AddTagHelpersAsServices();
            mvcBuilder.AddViewComponentsAsServices();
            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.AllowDuplicateProperties = false;
                options.JsonSerializerOptions.AllowTrailingCommas = false;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultBufferSize = 4096;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreReadOnlyFields = false;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                options.JsonSerializerOptions.IncludeFields = false;
                options.JsonSerializerOptions.MaxDepth = 64;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
                options.JsonSerializerOptions.PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
                options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            serviceCollection.AddControllers();
            serviceCollection.AddControllersWithViews();
            serviceCollection.AddRazorPages();
        }

        return serviceCollection;
    }
    /// <summary>
    /// Configure the web application to use mvc features.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to configure.
    /// </param>
    public static WebApplication UseMvcFeatures(this WebApplication webApplication)
    {
        var featureEnabled = webApplication.Configuration.GetValue<Boolean>("features:mvc", false);

        if (featureEnabled)
        {
            webApplication.MapControllers();
            webApplication.MapRazorPages();
        }

        return webApplication;
    }
}
