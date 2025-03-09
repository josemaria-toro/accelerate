using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Configuration options for data repositories based on Cosmos DB.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
public abstract class CosmosRepositoryOptions : EntityFrameworkRepositoryOptions
{
    /// <summary>
    /// Database name.
    /// </summary>
    public String Database { get; set; }
}