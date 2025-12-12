using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Reflection;
using Zetatech.Accelerate.Domain;
using Zetatech.Accelerate.Domain.Abstractions;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base class for implementing custom repository context based on Entity Framework.
/// </summary>
/// <typeParam name="TEntity">
/// The type of entity managed by the context. Must inherit from <see cref="BaseEntity"/>.
/// </typeParam>
/// <typeParam name="TOptions">
/// The type of configuration options. Must inherit from <see cref="BaseRepositoryOptions"/>.
/// </typeParam>
public abstract class BaseRepositoryContext<TEntity, TOptions> : DbContext where TEntity : BaseEntity
                                                                           where TOptions : BaseRepositoryOptions
{
    private Boolean _disposed;
    private TOptions _options;
    private IRepository<TEntity> _repository;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="repository">
    /// The repository associated with this context.
    /// </param>
    /// <param name="options">
    /// The configuration options for this repository context.
    /// </param>
    protected BaseRepositoryContext(IRepository<TEntity> repository, TOptions options)
    {
        _options = options;
        _repository = repository;

        if (ChangeTracker != null)
        {
            ChangeTracker.AutoDetectChangesEnabled = _options.TrackChanges;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.LazyLoadingEnabled = _options.LazyLoading;

            if (_options.TrackChanges)
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
    /// Gets the configuration options of this repository context.
    /// </summary>
    protected TOptions Options => _options;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose();

        if (disposing)
        {
            _options = null;
            _repository = null;
        }
    }
    /// <summary>
    /// Get the data source name.
    /// </summary>
    public abstract String GetDataSource();
    /// <summary>
    /// Configures the database and other options for this context.
    /// </summary>
    /// <param name="optionsBuilder">
    /// A builder used to create or modify options for this context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(x => { x.Default(WarningBehavior.Log); })
                      .EnableDetailedErrors(_options.DetailedErrors)
                      .EnableSensitiveDataLogging(_options.SensitiveDataLogging);
    }
    /// <summary>
    /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">
    /// The builder being used to construct the model for this context.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _repository.GetType()
                   .GetMethod("OnModelCreating", BindingFlags.Instance | BindingFlags.NonPublic)
                   .Invoke(_repository, [modelBuilder]);
    }
}