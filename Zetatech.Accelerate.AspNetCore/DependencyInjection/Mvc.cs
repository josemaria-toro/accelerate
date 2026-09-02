using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.DependencyInjection;

public static partial class DependencyInjection
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
            var serializerOptions = Json.GetSerializerOptions();

            options.JsonSerializerOptions.AllowDuplicateProperties = serializerOptions.AllowDuplicateProperties;
            options.JsonSerializerOptions.AllowTrailingCommas = serializerOptions.AllowTrailingCommas;
            options.JsonSerializerOptions.DefaultBufferSize = serializerOptions.DefaultBufferSize;
            options.JsonSerializerOptions.DefaultIgnoreCondition = serializerOptions.DefaultIgnoreCondition;
            options.JsonSerializerOptions.DictionaryKeyPolicy = serializerOptions.DictionaryKeyPolicy;
            options.JsonSerializerOptions.IgnoreReadOnlyFields = serializerOptions.IgnoreReadOnlyFields;
            options.JsonSerializerOptions.IgnoreReadOnlyProperties = serializerOptions.IgnoreReadOnlyProperties;
            options.JsonSerializerOptions.IncludeFields = serializerOptions.IncludeFields;
            options.JsonSerializerOptions.MaxDepth = serializerOptions.MaxDepth;
            options.JsonSerializerOptions.NumberHandling = serializerOptions.NumberHandling;
            options.JsonSerializerOptions.PreferredObjectCreationHandling = serializerOptions.PreferredObjectCreationHandling;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = serializerOptions.PropertyNameCaseInsensitive;
            options.JsonSerializerOptions.PropertyNamingPolicy = serializerOptions.PropertyNamingPolicy;
            options.JsonSerializerOptions.ReadCommentHandling = serializerOptions.ReadCommentHandling;
            options.JsonSerializerOptions.UnknownTypeHandling = serializerOptions.UnknownTypeHandling;
            options.JsonSerializerOptions.UnmappedMemberHandling = serializerOptions.UnmappedMemberHandling;
            options.JsonSerializerOptions.WriteIndented = serializerOptions.WriteIndented;

            foreach (var converter in serializerOptions.Converters)
            {
                options.JsonSerializerOptions.Converters.Add(converter);
            }
        });

        serviceCollection.AddControllers();
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddRazorPages();

        return serviceCollection;
    }
    public static IEndpointRouteBuilder UseMvcComponents(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllers();
        endpointRouteBuilder.MapRazorPages();

        return endpointRouteBuilder;
    }
}
