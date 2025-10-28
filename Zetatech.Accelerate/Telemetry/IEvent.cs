using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom events.
/// </summary>
public interface IEvent : ICloneable
{
    /// <summary>
    /// Gets or sets the name of the event.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related event information.
    /// </summary>
    Guid OperationId { get; set; }
}