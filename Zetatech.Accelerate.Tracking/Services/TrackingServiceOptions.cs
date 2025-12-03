using System;
using Zetatech.Accelerate.Tracking.Abstractions;

namespace Zetatech.Accelerate.Tracking.Services;

/// <summary>
/// Represents the options for configuring the tracking service.
/// </summary>
public sealed class TrackingServiceOptions : BaseTrackingServiceOptions
{
    /// <summary>
    /// Gets or sets the connection string with the message broker.
    /// </summary>
    public String ConnectionString { get; set; }
}