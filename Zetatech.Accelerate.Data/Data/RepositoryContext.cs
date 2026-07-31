using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Enums;

namespace Zetatech.Accelerate.Data;

internal sealed class RepositoryContext<TEntity> : DbContext where TEntity : class, IEntity, new()
{
    private Boolean _disposed;
    private readonly RepositoryOptions _options;

    public RepositoryContext(IOptions<RepositoryOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));

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
            case DatabaseEngines.AzureSql:
                optionsBuilder.UseAzureSql(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case DatabaseEngines.InMemory:
                optionsBuilder.UseInMemoryDatabase(_options.ConnectionString);
                break;
            case DatabaseEngines.PostgreSql:
                optionsBuilder.UseNpgsql(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case DatabaseEngines.Sqlite:
                optionsBuilder.UseSqlite(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case DatabaseEngines.SqlServer:
                optionsBuilder.UseSqlServer(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
            case DatabaseEngines.Synapse:
                optionsBuilder.UseAzureSynapse(_options.ConnectionString, options => { options.CommandTimeout(_options.Timeout); });
                break;
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<TEntity>();
    public override String ToString() => Database.GenerateCreateScript();
}