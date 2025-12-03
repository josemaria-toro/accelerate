using System;

namespace Zetatech.Accelerate.Tracking;

/// <summary>
/// Represents an application trace.
/// </summary>
public sealed class Trace
{
    /// <summary>
    /// Gets or sets the exception with the information about the error.
    /// </summary>
    public Exception Exception { get; set; }
    /// <summary>
    /// Gets or sets the message of the trace.
    /// </summary>
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets the operation identifier used to associate related trace.
    /// </summary>
    public Guid OperationId { get; set; }
    /// <summary>
    /// Gets or sets the severity of the trace.
    /// </summary>
    public Severities Severity { get; set; }
    /// <summary>
    /// Gets or sets the source of the trace.
    /// </summary>
    public String SourceTypeName { get; set; }
}
