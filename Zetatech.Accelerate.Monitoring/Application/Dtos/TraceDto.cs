using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents a trace recorded during application execution, providing structured diagnostic information for runtime events.
/// </summary>
public sealed class TraceEntity : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the application identifier associated with the trace.
    /// </summary>
    [JsonPropertyName("appId")]
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the category or context under which the trace was captured.
    /// </summary>
    [JsonPropertyName("category")]
    public String Category { get; set; }
    /// <summary>
    /// Gets or sets the name of the device or host where the event occurred.
    /// </summary>
    [JsonPropertyName("device")]
    public String Device { get; set; }
    /// <summary>
    /// Gets or sets the event name associated with the logging entry.
    /// </summary>
    [JsonPropertyName("event")]
    public String Event { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the logging entry.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the full message content describing the logged event.
    /// </summary>
    [JsonPropertyName("message")]
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets the operating system version on which the logging entry was recorded.
    /// </summary>
    [JsonPropertyName("operatingSystem")]
    public String OperatingSystem { get; set; }
    /// <summary>
    /// Gets or sets the unique operation identifier grouping related logging entries.
    /// </summary>
    [JsonPropertyName("operationId")]
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the name of the platform (e.g. Windows, Linux) where the event occurred.
    /// </summary>
    [JsonPropertyName("platform")]
    public String Platform { get; set; }
    /// <summary>
    /// Gets or sets the numeric severity level of the logging entry.
    /// </summary>
    [JsonPropertyName("severityLevel")]
    public Int32 SeverityLevel { get; set; }
    /// <summary>
    /// Gets or sets the UTC timestamp when the logging entry was recorded.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}