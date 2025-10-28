using System;

namespace Zetatech.Accelerate.Logging.Providers;

/// <summary>
/// Represents the options for configuring the RabbitMQ-based logging provider.
/// </summary>
public sealed class RabbitMqLoggingProviderOptions : BaseLoggingProviderOptions
{
    /// <summary>
    /// Gets or sets the connection string to connect with the RabbitMQ server.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets the routing key for the errors messages.
    /// </summary>
    public String ErrorsRk { get; set; }
    /// <summary>
    /// Gets or sets the exchange name.
    /// </summary>
    public String Exchange { get; set; }
    /// <summary>
    /// Gets or sets the routing key for the traces messages.
    /// </summary>
    public String TracesRk { get; set; }
}