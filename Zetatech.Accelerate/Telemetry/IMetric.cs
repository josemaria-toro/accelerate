using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom metrics.
/// </summary>
public interface IMetric : ICloneable
{
    /// <summary>
    /// Gets or sets the dimension or category associated with the metric.
    /// </summary>
    String Dimension { get; set; }
    /// <summary>
    /// Gets or sets the name of the metric.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related metric information.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the value of the metric.
    /// </summary>
    Double Value { get; set; }
}