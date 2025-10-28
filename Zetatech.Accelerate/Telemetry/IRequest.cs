using System;
using System.Net;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom HTTP requests.
/// </summary>
public interface IRequest : ICloneable
{
    /// <summary>
    /// Gets or sets the duration of the HTTP request.
    /// </summary>
    TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the IP address from which the HTTP request originated.
    /// </summary>
    IPAddress IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the name or identifier of the HTTP request.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related HTTP request information.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the HTTP response code returned for the HTTP request.
    /// </summary>
    HttpStatusCode ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request was successful.
    /// </summary>
    Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the timestamp indicating when the HTTP request is tracked.
    /// </summary>
    DateTime Timestamp { get; set; }
    /// <summary>
    /// Gets or sets the URI associated with the HTTP request.
    /// </summary>
    Uri Uri { get; set; }
}