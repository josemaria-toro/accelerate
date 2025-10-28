using System;

namespace Zetatech.Accelerate.Messaging.Publishers;

/// <summary>
/// Represents the options for configuring RabbitMQ-based message publisher.
/// </summary>
public class RabbitMqPublisherServiceOptions : BasePublisherServiceOptions
{
    /// <summary>
    /// Gets or sets the exchange name.
    /// </summary>
    public String Exchange { get; set; }
    /// <summary>
    /// Gets or sets the connection string with RabbitMQ server.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets the routing key.
    /// Gets or sets the routing key.
    /// </summary>
    public String RoutingKey { get; set; }
}
