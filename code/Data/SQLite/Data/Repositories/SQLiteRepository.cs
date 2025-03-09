using Accelerate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Data depository based on SQLite.
/// </summary>
/// <typeparam name="TEntity">
/// Type of data entity managed in the repository.
/// </typeparam>
[ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
public abstract class SQLiteRepository<TEntity> : EntityFrameworkRepository<TEntity, SQLiteRepositoryOptions> where TEntity : class, IEntity, new()
{
    private Boolean _disposed;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for data repository.
    /// </param>
    protected SQLiteRepository(IOptions<SQLiteRepositoryOptions> options) : base(options)
    {
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        base.Dispose(disposing);

        _disposed = true;
    }
    /// <summary>
    /// Configure the database (and other options) to be used for the context.
    /// </summary>
    /// <param name="optionsBuilder">
    /// A builder used to create or modify options for this context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite(Options.ConnectionString, options =>
        {
            options.CommandTimeout(Options.Timeout);
        });
    }
}