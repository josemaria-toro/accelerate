using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Extensions;

public static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions GetDefaultOptions(this JsonSerializerOptions jsonSerializerOptions)
    {
        jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowDuplicateProperties = false,
            AllowTrailingCommas = false,
            DefaultBufferSize = 4096,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = false,
            MaxDepth = 64,
            NumberHandling = JsonNumberHandling.Strict,
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
            WriteIndented = false
        };

        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        return jsonSerializerOptions;
    }

}
