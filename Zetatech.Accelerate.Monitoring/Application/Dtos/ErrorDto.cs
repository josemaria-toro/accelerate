using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents a custom data n application error.
/// </summary>
public sealed class ErrorDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the application identifier associated with the application error.
    /// </summary>
    [JsonPropertyName("appId")]
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the category or context under which the application error was captured.
    /// </summary>
    [JsonPropertyName("category")]
    public String Category { get; set; }
    /// <summary>
    /// Gets or sets the name of the device or host where the application error occurred.
    /// </summary>
    [JsonPropertyName("device")]
    public String Device { get; set; }
    /// <summary>
    /// Gets or sets the event name associated with the application error.
    /// </summary>
    [JsonPropertyName("event")]
    public String Event { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the application error.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the full message content describing the application error.
    /// </summary>
    [JsonPropertyName("message")]
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets the operating system version on which the application error was recorded.
    /// </summary>
    [JsonPropertyName("operatingSystem")]
    public String OperatingSystem { get; set; }
    /// <summary>
    /// Gets or sets the unique operation identifier grouping related entries.
    /// </summary>
    [JsonPropertyName("operationId")]
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the name of the platform (e.g. Windows, Linux) where the application error occurred.
    /// </summary>
    [JsonPropertyName("platform")]
    public String Platform { get; set; }
    /// <summary>
    /// Gets or sets the numeric severity level of the application error.
    /// </summary>
    [JsonPropertyName("severityLevel")]
    public Int32 SeverityLevel { get; set; }
    /// <summary>
    /// Gets or sets the full stack trace information of the application error.
    /// </summary>
    [JsonPropertyName("stackTrace")]
    public String StackTrace { get; set; }
    /// <summary>
    /// Gets or sets the UTC timestamp when the application error was recorded.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// Gets or sets the fully qualified name of the application error type.
    /// </summary>
    [JsonPropertyName("typeName")]
    public String TypeName { get; set; }
}