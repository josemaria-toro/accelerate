using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the results for a dependency call.
/// </summary>
public sealed class DependencyDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the duration of the dependency call.
    /// </summary>
    [JsonPropertyName("duration")]
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the command or data sended to the dependency call.
    /// </summary>
    [JsonPropertyName("inputData")]
    public String InputData { get; set; }
    /// <summary>
    /// Gets or sets the output data returned by the dependency call.
    /// </summary>
    [JsonPropertyName("outputData")]
    public String OutputData { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the dependency call was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the target system or endpoint of the dependency call.
    /// </summary>
    [JsonPropertyName("target")]
    public String Target { get; set; }
    /// <summary>
    /// Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).
    /// </summary>
    [JsonPropertyName("type")]
    public String Type { get; set; }
}
