using System;
using Zetatech.Accelerate.Messaging;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base class for implementing custom messages.
/// </summary>
public abstract class BaseMessage : IMessage
{
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related message.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the timestamp when the message was published.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Creates a shallow copy of the current object instance.
    /// </summary>
    public virtual Object Clone()
    {
        var type = GetType();
        var instance = Activator.CreateInstance(type);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            property.SetValue(instance, value);
        }

        return instance;
    }
}
