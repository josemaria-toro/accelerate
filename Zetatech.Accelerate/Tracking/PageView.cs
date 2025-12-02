using System;
using System.Net;

namespace Zetatech.Accelerate.Tracking;

/// <summary>
/// Represents a page view.
/// </summary>
public sealed class PageView
{
    /// <summary>
    /// Gets or sets the name of the device.
    /// </summary>
    public String DeviceName { get; set; }
    /// <summary>
    /// Gets or sets the name of the page viewed.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the duration of the page view.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the ip address of the device.
    /// </summary>
    public IPAddress IpAddress { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related page view information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the URI of the page viewed.
    /// </summary>
    public Uri Uri { get; set; }
    /// <summary>
    /// Gets or sets the user agent of the device.
    /// </summary>
    public String UserAgent { get; set; }
}