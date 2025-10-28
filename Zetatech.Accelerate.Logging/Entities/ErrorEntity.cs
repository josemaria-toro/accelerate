using System;

namespace Zetatech.Accelerate.Logging.Entities;

/// <summary>
/// Represents a structured log entry containing details about an error, captured and stored in the diagnostics schema for observability and analytics.
/// </summary>
internal sealed class ErrorEntity : LoggingEntity
{
    /// <summary>
    /// Gets or sets the full stack trace information of the error.
    /// </summary>
    public String StackTrace { get; set; }
    /// <summary>
    /// Gets or sets the fully qualified name of the error type.
    /// </summary>
    public String TypeName { get; set; }
}