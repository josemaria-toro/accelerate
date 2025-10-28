using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the result of an availability test.
/// </summary>
public sealed class AvailabilityDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the duration of the availability test.
    /// </summary>
    [JsonPropertyName("duration")]
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the message associated with the availability test result.
    /// </summary>
    [JsonPropertyName("message")]
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the availability test was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public Boolean Success { get; set; }
}
