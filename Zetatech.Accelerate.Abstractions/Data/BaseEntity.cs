using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Data;

/// <summary>
/// Represents the base class for implementing custom entities.
/// </summary>
[JsonSourceGenerationOptions(AllowTrailingCommas = false,
                             Converters = [typeof(JsonStringEnumConverter)],
                             DefaultBufferSize = 4096,
                             DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                             DictionaryKeyPolicy = JsonKnownNamingPolicy.CamelCase,
                             GenerationMode = JsonSourceGenerationMode.Default,
                             IgnoreReadOnlyFields = false,
                             IgnoreReadOnlyProperties = false,
                             IncludeFields = false,
                             MaxDepth = 64,
                             NumberHandling = JsonNumberHandling.Strict,
                             PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,
                             PropertyNameCaseInsensitive = true,
                             PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
                             ReadCommentHandling = JsonCommentHandling.Skip,
                             UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
                             UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                             UseStringEnumConverter = true,
                             WriteIndented = true)]
public abstract class BaseEntity : BaseCloneable, IEntity
{
}