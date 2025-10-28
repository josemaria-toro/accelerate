using System;

namespace Zetatech.Accelerate.Telemetry.Messages;

/// <summary>
/// Represents the data for a metric.
/// </summary>
internal sealed class MetricMessage : TelemetryMessage
{
    /// <summary>
    /// Gets or sets the dimension or category associated with the metric.
    /// </summary>
    public String Dimension { get; set; }
    /// <summary>
    /// Gets or sets the value of the metric.
    /// </summary>
    public Double Value { get; set; }
}