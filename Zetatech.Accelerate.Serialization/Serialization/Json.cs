using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Serialization.Converters;

namespace Zetatech.Accelerate.Serialization;

public static partial class Json
{
    private static JsonSerializerOptions _jsonSerializerOptions;

    public static JsonSerializerOptions GetSerializerOptions()
    {
        if (_jsonSerializerOptions == null)
        {
            _jsonSerializerOptions = new JsonSerializerOptions
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

            _jsonSerializerOptions.Converters.Add(new JsonByteArrayConverter());
            _jsonSerializerOptions.Converters.Add(new JsonIPAddressConverter());
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            _jsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
        }

        return _jsonSerializerOptions;
    }
    public static String ToString(Object jsonObject)
    {
        return JsonSerializer.Serialize(jsonObject, GetSerializerOptions());
    }
    public static TObject ToObject<TObject>(Byte[] jsonBuffer)
    {
        return JsonSerializer.Deserialize<TObject>(jsonBuffer, GetSerializerOptions());
    }
    public static TObject ToObject<TObject>(String jsonString)
    {
        return JsonSerializer.Deserialize<TObject>(jsonString, GetSerializerOptions());
    }
}
