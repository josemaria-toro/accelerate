using System;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Represents the base options for configuring a logging provider.
/// </summary>
public abstract class BaseLoggingProviderOptions : BaseCloneable
{
    /// <summary>
    /// Gets or sets the application id associated with the logging provider.
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