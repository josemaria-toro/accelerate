using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Provides the interface for implementing custom dependency calls.
/// </summary>
public interface IDependency : ICloneable
{
    /// <summary>
    /// Gets or sets the duration of the dependency call.
    /// </summary>
    TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the command or data sended to the dependency call.
    /// </summary>
    String InputData { get; set; }
    /// <summary>
    /// Gets or sets the name of the dependency call.
    /// </summary>
    String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related dependency call information.
    /// </summary>
    Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the output data returned by the dependency call.
    /// </summary>
    String OutputData { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the dependency call was successful.
    /// </summary>
    Boolean Success { get; set; }
    /// <summary>
    /// Gets or sets the target system or endpoint of the dependency call.
    /// </summary>
    String Target { get; set; }
    /// <summary>
    /// Gets or sets the type of the dependency call (e.g., SQL, HTTP, etc.).
    /// </summary>
    String Type { get; set; }
}