using System;
using Zetatech.Accelerate.Messaging.Abstractions;

namespace Zetatech.Accelerate.Messaging.Services;

/// <summary>
/// Represents the options for configuring the message publisher.
/// </summary>
public class RabbitMqPublisherServiceOptions : BasePublisherServiceOptions
{
    /// <summary>
    /// Gets or sets the exchange name.
    /// </summary>
    public String Exchange { get; set; }
    /// <summary>
    /// Gets or sets the connection string with the message broker.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets the routing key.
    /// </summary>
    public String RoutingKey { get; set; }
}
