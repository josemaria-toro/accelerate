using Accelerate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Represents a session with the database and can be used to query and save instances of your entities.
/// </summary>
/// <typeparam name="TEntity">
/// Type of data entity.
/// </typeparam>
/// <typeparam name="TOptions">
/// Type of configuration options.
/// </typeparam>
[ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
internal class EntityFrameworkRepositoryContext<TEntity, TOptions> : DbContext where TEntity : class, IEntity, new()
                                                                               where TOptions : EntityFrameworkRepositoryOptions
{
    private Boolean _disposed;
    private readonly EntityFrameworkRepository<TEntity, TOptions> _repository;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="repository">
    /// Data repository managed in the context.
    /// </param>
    public EntityFrameworkRepositoryContext(EntityFrameworkRepository<TEntity, TOptions> repository)
    {
        _repository = repository ?? throw new ArgumentException($"Argument '{nameof(repository)}' cannot be null or empty", nameof(repository));

        if (ChangeTracker != null)
        {
            ChangeTracker.AutoDetectChangesEnabled = _repository.Options.TrackChanges;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.LazyLoadingEnabled = _repository.Options.LazyLoading;

            if (_repository.Options.TrackChanges)
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            }
            else
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }

        Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        Dispose(true);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

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

        optionsBuilder.ConfigureWarnings(x => { x.Default(WarningBehavior.Log); })
                      .EnableDetailedErrors(_repository.Options.DetailedErrors)
                      .EnableSensitiveDataLogging(_repository.Options.SensitiveDataLogging);

        _repository.OnConfiguring(optionsBuilder);
    }
    /// <summary>
    /// Configure the model that was discovered by convention from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">
    /// The builder being used to construct the model for this context.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _repository.OnModelCreating(modelBuilder);
    }
}