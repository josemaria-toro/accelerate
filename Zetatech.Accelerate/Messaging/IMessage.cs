using System;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Provides the interface for implementing custom message.
/// </summary>
public interface IMessage : ICloneable
{
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related message.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the timestamp when the message was published.
    /// </summary>
    DateTime Timestamp { get; set; }
}
