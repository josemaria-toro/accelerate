using System;
using System.Collections.Generic;
using Zetatech.Accelerate.Infrastructure.Abstractions;

namespace Zetatech.Accelerate.Infrastructure.Tracking;

/// <summary>
/// Represents a message for tracking purposes.
/// </summary>
public sealed class TrackingMessage : BaseMessage
{
    /// <summary>
    /// Gets or sets the type of the message.
    /// </summary>
    public TrackingMessageTypes MessageType { get; set; }
    /// <summary>
    /// Gets or sets the properties associated with the message.
    /// </summary>
    public IDictionary<String, String> Properties { get; set; }
}
