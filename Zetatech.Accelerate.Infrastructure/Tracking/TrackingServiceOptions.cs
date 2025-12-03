using System;
using Zetatech.Accelerate.Infrastructure.Abstractions;

namespace Zetatech.Accelerate.Infrastructure.Tracking;

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