using System;
using Zetatech.Accelerate.Infrastructure.Abstractions;

namespace Zetatech.Accelerate.Infrastructure.Messaging;

/// <summary>
/// Represents the options for configuring the message subscriber.
/// </summary>
public class RabbitMqSubscriberServiceOptions : BaseSubscriberServiceOptions
{
    /// <summary>
    /// Gets or sets the connection string with the message broker.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets if the queue allow more than 1 subscribers.
    /// </summary>
    public Boolean Exclusive { get; set; }
    /// <summary>
    /// Gets or sets the name of the queue.
    /// </summary>
    public String QueueName { get; set; }
}
