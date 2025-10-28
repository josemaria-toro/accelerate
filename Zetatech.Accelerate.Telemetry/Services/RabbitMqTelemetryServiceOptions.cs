using System;

namespace Zetatech.Accelerate.Telemetry.Services;

/// <summary>
/// Represents the options for configuring RabbitMQ-based telemetry service.
/// </summary>
public sealed class RabbitMqTelemetryServiceOptions : BaseTelemetryServiceOptions
{
    /// <summary>
    /// Gets or sets the routing key for availability tests.
    /// </summary>
    public String AvailabilityRk { get; set; }
    /// <summary>
    /// Gets or sets the connection string to connect with the RabbitMQ server.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets the routing key for dependencies.
    /// </summary>
    public String DependenciesRk { get; set; }
    /// <summary>
    /// Gets or sets the routing key for events.
    /// </summary>
    public String EventsRk { get; set; }
    /// <summary>
    /// Gets or sets the exchange name.
    /// </summary>
    public String Exchange { get; set; }
    /// <summary>
    /// Gets or sets the routing key for metrics.
    /// </summary>
    public String MetricsRk { get; set; }
    /// <summary>
    /// Gets or sets the routing key for page views.
    /// </summary>
    public String PageViewsRk { get; set; }
    /// <summary>
    /// Gets or sets the routing key for HTTP requests.
    /// </summary>
    public String RequestsRk { get; set; }
}