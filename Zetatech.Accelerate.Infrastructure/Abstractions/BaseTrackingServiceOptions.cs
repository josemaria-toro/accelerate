using System;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base options for configuring a tracking service.
/// </summary>
public abstract class BaseTrackingServiceOptions
{
    /// <summary>
    /// Gets or sets the application id associated with the tracking service.
    /// </summary>
    public Guid AppId { get; set; }
}
