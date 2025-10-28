using System;

namespace Zetatech.Accelerate.Telemetry.Entities;

/// <summary>
/// Represents the data for an HTTP request.
/// </summary>
internal sealed class RequestEntity : TelemetryEntity
{
    /// <summary>
    /// Gets or sets the duration of the HTTP request.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the IP address from which the HTTP request originated.
    /// </summary>
    public String IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the HTTP response code returned for the HTTP request.
    /// </summary>
    public Int32 ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request was successful.
    /// </summary>
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the URL associated with the HTTP request.
    /// </summary>
    public String Url { get; set; }
}
