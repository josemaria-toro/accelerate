using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zetatech.Accelerate.Data.Abstractions;
using Zetatech.Accelerate.Data.Enums;

namespace Zetatech.Accelerate.Data.Contexts;

public sealed class EntityFrameworkContext<TEntity, TOptions> : DbContext where TEntity : class, IEntity, new()
                                                                          where TOptions : BaseEntityFrameworkRepositoryOptions
{
    private Boolean _disposed;
    private readonly TOptions _options;

    internal EntityFrameworkContext(TOptions options)
    {
        _options = options ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));

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

    public override void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose();

        GC.SuppressFinalize(this);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(x => { x.Default(WarningBehavior.Log); })
                      .EnableDetailedErrors(false)
                      .EnableSensitiveDataLogging(false);

        switch (_options.Engine)
        {
            case SupportedDatabaseEngines.AzureSql:
                optionsBuilder.UseAzureSql(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case SupportedDatabaseEngines.InMemory:
                optionsBuilder.UseInMemoryDatabase(_options.ConnectionString);
                break;
            case SupportedDatabaseEngines.PostgreSql:
                optionsBuilder.UseNpgsql(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case SupportedDatabaseEngines.Sqlite:
                optionsBuilder.UseSqlite(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case SupportedDatabaseEngines.SqlServer:
                optionsBuilder.UseSqlServer(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case SupportedDatabaseEngines.Synapse:
                optionsBuilder.UseAzureSynapse(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<TEntity>();
}
