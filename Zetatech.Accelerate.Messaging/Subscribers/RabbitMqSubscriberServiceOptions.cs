using System;

namespace Zetatech.Accelerate.Messaging.Subscribers;

/// <summary>
/// Represents the options for configuring RabbitMQ-based message subscriber.
/// </summary>
public class RabbitMqSubscriberServiceOptions : BaseSubscriberServiceOptions
{
    /// <summary>
    /// Gets or sets the connection string with RabbitMQ server.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets the name of the queue.
    /// </summary>
    public String Queue { get; set; }
}
