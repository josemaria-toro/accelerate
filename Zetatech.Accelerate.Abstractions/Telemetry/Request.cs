﻿using System;
using System.Net;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the data for an HTTP request.
/// </summary>
public class Request : BaseCloneable, IRequest
{
    /// <summary>
    /// Gets or sets the duration of the HTTP request.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the IP address from which the HTTP request originated.
    /// </summary>
    public IPAddress IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the name or identifier of the HTTP request.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related HTTP request information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the HTTP response code returned for the HTTP request.
    /// </summary>
    public HttpStatusCode ResponseCode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request was successful.
    /// </summary>
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the timestamp indicating when the HTTP request is tracked.
    /// </summary>
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// Gets or sets the URI associated with the HTTP request.
    /// </summary>
    public Uri Uri { get; set; }
}