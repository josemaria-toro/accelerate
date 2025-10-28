using System;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Messaging;

namespace Zetatech.Accelerate.Logging.Messages;

/// <summary>
/// Represents the base class for implementing custom logging messages.
/// </summary>
internal abstract class LoggingMessage : BaseMessage
{
    /// <summary>
    /// Gets or sets the application identifier associated with the logging entry.
    /// </summary>
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets the logging category or context under which the logging entry was captured.
    /// </summary>
    public String Category { get; set; }
    /// <summary>
    /// Gets or sets the name of the device or host where the event occurred.
    /// </summary>
    public String Device { get; set; }
    /// <summary>
    /// Gets or sets the event name associated with the logging entry.
    /// </summary>
    public String Event { get; set; }
    /// <summary>
    /// Gets or sets the full logging entry message content describing the logged event.
    /// </summary>
    public String Message { get; set; }
    /// <summary>
    /// Gets or sets the operating system version on which the logging entry was recorded.
    /// </summary>
    public String OperatingSystem { get; set; }
    /// <summary>
    /// Gets or sets the name of the platform (e.g. Windows, Linux) where the event occurred.
    /// </summary>
    public String Platform { get; set; }
    /// <summary>
    /// Gets or sets the numeric severity level of the logging entry.
    /// </summary>
    public LogLevel SeverityLevel { get; set; }
}
