using System;

namespace Zetatech.Accelerate.Telemetry.Entities;

/// <summary>
/// Represents the data for a metric.
/// </summary>
internal sealed class MetricEntity : TelemetryEntity
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
