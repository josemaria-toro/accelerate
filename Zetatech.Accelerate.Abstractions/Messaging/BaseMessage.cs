using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Represents the base class for implementing custom messages.
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
public class BaseMessage : BaseCloneable, IMessage
{
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related message.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the timestamp when the message was published.
    /// </summary>
    public DateTime Timestamp { get; set; }
}
