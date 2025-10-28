using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Zetatech.Accelerate.Monitoring.DependencyInjection;

public static class Serializers
{
    public static IServiceCollection AddJsonSerializer(this IServiceCollection serviceCollection)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            DefaultBufferSize = 4096,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Default,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = false,
            MaxDepth = 64,
            NumberHandling = JsonNumberHandling.Strict,
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
            WriteIndented = true
        };

        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        return serviceCollection.AddSingleton(jsonSerializerOptions);
    }
}
