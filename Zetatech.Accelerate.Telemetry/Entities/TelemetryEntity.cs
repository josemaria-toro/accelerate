using System;
using Zetatech.Accelerate.Data;

namespace Zetatech.Accelerate.Telemetry.Entities;

/// <summary>
/// Represents the base class for implementing custom telemetry entities.
/// </summary>
internal abstract class TelemetryEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the application identifier associated with the telemetry entry.
    /// </summary>
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the telemetry entry.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the telemetry entry.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related telemetry information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the timestamp indicating when the telemetry entry is tracked.
    /// </summary>
    public DateTime Timestamp { get; set; }
}
