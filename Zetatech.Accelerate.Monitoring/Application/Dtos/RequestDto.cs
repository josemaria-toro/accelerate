using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the data for an HTTP request.
/// </summary>
public sealed class RequestDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the duration of the HTTP request.
    /// </summary>
    [JsonPropertyName("duration")]
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the IP address from which the HTTP request originated.
    /// </summary>
    [JsonPropertyName("ipAddress")]
    public String IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the HTTP response code returned for the HTTP request.
    /// </summary>
    [JsonPropertyName("responseCode")]
    public Int32 ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the URL associated with the HTTP request.
    /// </summary>
    [JsonPropertyName("url")]
    public String Url { get; set; }
}
