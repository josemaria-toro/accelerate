using System;

namespace Zetatech.Accelerate.Logging.Messages;

/// <summary>
/// Represents the contents of a message for errors.
/// </summary>
internal sealed class ErrorMessage : LoggingMessage
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
