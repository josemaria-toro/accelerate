using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the data for an event.
/// </summary>
public class Event : BaseCloneable, IEvent
{
    /// <summary>
    /// Gets or sets the name of the event.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related event information.
    /// </summary>
    public Guid OperationId { get; set; }
}