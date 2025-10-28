using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the data for a metric.
/// </summary>
public class Metric : BaseCloneable, IMetric
{
    /// <summary>
    /// Gets or sets the dimension or category associated with the metric.
    /// </summary>
    public String Dimension { get; set; }
    /// <summary>
    /// Gets or sets the name of the metric.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related metric information.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the value of the metric.
    /// </summary>
    public Double Value { get; set; }
}