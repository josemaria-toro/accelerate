using Microsoft.EntityFrameworkCore;
using Zetatech.Accelerate.Domain.Abstractions;
using Zetatech.Accelerate.Infrastructure.Abstractions;

namespace Zetatech.Accelerate.Infrastructure.Persistency;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based repository context.
/// </summary>
/// <typeParam name="TEntity">
/// The type of entity managed by the context. Must inherit from <see cref="BaseEntity"/>.
/// </typeParam>
/// <typeParam name="TOptions">
/// The type of configuration options. Must inherit from <see cref="PostgreSqlRepositoryOptions"/>.
/// </typeParam>
public sealed class PostgreSqlRepositoryContext<TEntity, TOptions> : BaseRepositoryContext<TEntity, TOptions> where TEntity : BaseEntity
                                                                                                              where TOptions : PostgreSqlRepositoryOptions
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="repository">
    /// The repository associated with this context.
    /// </param>
    /// <param name="options">
    /// The repository associated with this context.
    /// </param>
    public PostgreSqlRepositoryContext(PostgreSqlRepository<TEntity, TOptions> repository, TOptions options) : base(repository, options)
    {
    }

    /// <summary>
    /// Configures the database and other options for this context.
    /// </summary>
    /// <param name="optionsBuilder">
    /// A builder used to create or modify options for this context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(Options.ConnectionString, options =>
        {
            options.CommandTimeout(Options.Timeout);
        });
    }
}