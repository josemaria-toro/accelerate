using System;
using System.Text.Json.Serialization;
using Zetatech.Accelerate.Application;

namespace Zetatech.Monitoring.Application.Dtos;

/// <summary>
/// Represents the data for a metric.
/// </summary>
public sealed class MetricDto : BaseDataTransferObject
{
    /// <summary>
    /// Gets or sets the dimension or category associated with the metric.
    /// </summary>
    [JsonPropertyName("dimension")]
    public String Dimension { get; set; }
    /// <summary>
    /// Gets or sets the value of the metric.
    /// </summary>
    [JsonPropertyName("value")]
    public Double Value { get; set; }
}
