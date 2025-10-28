using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the base options for configuring a telemetry service.
/// </summary>
public abstract class BaseTelemetryServiceOptions : BaseCloneable
{
    /// <summary>
    /// Gets or sets the application id associated with the telemetry service.
    /// </summary>
    public Guid AppId { get; set; }
}
