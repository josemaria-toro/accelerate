using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the base class for implementing custom telemetry entities.
/// </summary>
public abstract class TelemetryDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the application identifier associated with the telemetry entry.
    /// </summary>
    [JsonPropertyName("appId")]
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the telemetry entry.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the telemetry entry.
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related telemetry information.
    /// </summary>
    [JsonPropertyName("operationId")]
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the timestamp indicating when the telemetry entry is tracked.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}
