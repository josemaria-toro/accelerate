using System;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base options for configuring a repository.
/// </summary>
public abstract class BaseRepositoryOptions
{
    /// <summary>
    /// Gets or sets the connection string used to connect with the data source.
    /// </summary>
    public String ConnectionString { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether detailed error messages are enabled for the repository.
    /// </summary>
    public Boolean DetailedErrors { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether lazy loading is enabled for related entities.
    /// </summary>
    public Boolean LazyLoading { get; set; }
    /// <summary>
    /// Gets or sets the schema where database is stored.
    /// </summary>
    public String Schema { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether sensitive data logging is enabled for the repository.
    /// </summary>
    public Boolean SensitiveDataLogging { get; set; }
    /// <summary>
    /// Gets or sets the timeout value, in seconds, for repository operations.
    /// </summary>
    public Int32 Timeout { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether change tracking is enabled for entities.
    /// </summary>
    public Boolean TrackChanges { get; set; }
}