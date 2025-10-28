using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Represents the base options for configuring a logging service.
/// </summary>
public abstract class BaseLoggingServiceOptions : BaseCloneable
{
    /// <summary>
    /// Gets or sets the application id associated with the logging service.
    /// </summary>
    public Guid AppId { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether critical level logging is enabled.
    /// </summary>
    public Boolean Critical { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether debug level logging is enabled.
    /// </summary>
    public Boolean Debug { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether error level logging is enabled.
    /// </summary>
    public Boolean Error { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether information level logging is enabled.
    /// </summary>
    public Boolean Information { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether warning level logging is enabled.
    /// </summary>
    public Boolean Warning { get; set; }
}