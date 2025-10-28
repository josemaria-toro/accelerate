using System;

namespace Zetatech.Accelerate.Logging.Providers;

/// <summary>
/// Represents the options for configuring the PostgreSQL-based logging provider.
/// </summary>
public sealed class PostgreSqlLoggingProviderOptions : BaseLoggingProviderOptions
{
    /// <summary>
    /// Gets or sets the connection string to connect with the database.
    /// </summary>
    public String ConnectionString { get; set; }
}