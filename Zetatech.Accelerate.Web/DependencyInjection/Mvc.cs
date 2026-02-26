using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Zetatech.Accelerate.DependencyInjection;

public static class Mvc
{
    public static IServiceCollection AddMvcComponents(this IServiceCollection serviceCollection)
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
            options.JsonSerializerOptions.WriteIndented = false;
        });

        serviceCollection.AddControllers();
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddRazorPages();

        return serviceCollection;
    }
    public static WebApplication UseMvcFeatures(this WebApplication webApplication)
    {
        webApplication.MapControllers();
        webApplication.MapRazorPages();

        return webApplication;
    }
}