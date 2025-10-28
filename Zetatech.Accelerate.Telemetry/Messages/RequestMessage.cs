using System;
using System.Net;

namespace Zetatech.Accelerate.Telemetry.Messages;

/// <summary>
/// Represents the data for an HTTP request.
/// </summary>
internal sealed class RequestMessage : TelemetryMessage
{
    /// <summary>
    /// Gets or sets the duration of the HTTP request.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the IP address from which the HTTP request originated.
    /// </summary>
    public IPAddress IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the HTTP response code returned for the HTTP request.
    /// </summary>
    public HttpStatusCode ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request was successful.
    /// </summary>
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the URI associated with the HTTP request.
    /// </summary>
    public Uri Uri { get; set; }
}