using System;
using Zetatech.Accelerate.Messaging;

namespace Zetatech.Accelerate.Telemetry.Messages;

/// <summary>
/// Represents the base class for implementing custom telemetry messages.
/// </summary>
internal abstract class TelemetryMessage : BaseMessage
{
    /// <summary>
    /// Gets or sets the application identifier associated with the message.
    /// </summary>
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the name of the message.
    /// </summary>
    public String Name { get; set; }
}