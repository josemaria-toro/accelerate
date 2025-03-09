using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Configuration options for data repositories based on SQLServer.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
public abstract class SQLServerRepositoryOptions : EntityFrameworkRepositoryOptions
{
}