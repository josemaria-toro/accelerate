using System;

namespace Zetatech.Accelerate.Telemetry.Entities;

/// <summary>
/// Represents the results for a dependency call.
/// </summary>
internal sealed class DependencyEntity : TelemetryEntity
{
    /// <summary>
    /// Gets or sets the duration of the dependency call.
    /// </summary>
    public Double Duration { get; set; }
    /// <summary>
    /// Gets or sets the command or data sended to the dependency call.
    /// </summary>
    public String InputData { get; set; }
    /// <summary>
    /// Gets or sets the output data returned by the dependency call.
    /// </summary>
    public String OutputData { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the dependency call was successful.
    /// </summary>
    public Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the target system or endpoint of the dependency call.
    /// </summary>
    public String Target { get; set; }
    /// <summary>
    /// Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).
    /// </summary>
    public String Type { get; set; }
}
