using System;

namespace Zetatech.Accelerate.Telemetry;

/// <summary>
/// Represents the results for a dependency call.
/// </summary>
public class Dependency : BaseCloneable, IDependency
{
    /// <summary>
    /// Gets or sets the duration of the dependency call.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// Gets or sets the command or data sended to the dependency call.
    /// </summary>
    public String InputData { get; set; }
    /// <summary>
    /// Gets or sets the name of the dependency call.
    /// </summary>
    public String Name { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related dependency call information.
    /// </summary>
    public Guid OperationId { get; set; }
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