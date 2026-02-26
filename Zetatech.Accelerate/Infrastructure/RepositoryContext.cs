using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zetatech.Accelerate.Domain.Abstractions;

namespace Zetatech.Accelerate.Infrastructure;

internal sealed class RepositoryContext<TEntity> : DbContext where TEntity : BaseEntity
{
    private Boolean _disposed;
    private RepositoryOptions _options;

    public RepositoryContext(RepositoryOptions options)
    {
        _options = options;

        if (ChangeTracker != null)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.LazyLoadingEnabled = true;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
    }

    public void Commit()
    {
        try
        {
            SaveChanges(true);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("An error is encountered while saving pending changes", ex);
        }
    }
    public override void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose();

        _options = null;

        GC.SuppressFinalize(this);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(x => { x.Default(WarningBehavior.Log); })
                      .EnableDetailedErrors(true)
                      .EnableSensitiveDataLogging(true);

        optionsBuilder.UseNpgsql(_options.ConnectionString, options =>
        {
            options.CommandTimeout(30);
        });
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TEntity>();
    }
    public void Rollback()
    {
        try
        {
            var entityEntries = ChangeTracker.Entries()
                                             .Where(x => x.State != EntityState.Unchanged);

            foreach (var entityEntry in entityEntries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entityEntry.State = EntityState.Unchanged;
                        break;
                }
            }

            SaveChanges(true);
        }
        catch (DbUpdateException ex)
        {
            throw new DataException("An error is encountered while undoing pending changes", ex);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while undoing pending changes", ex);
        }
    }
}