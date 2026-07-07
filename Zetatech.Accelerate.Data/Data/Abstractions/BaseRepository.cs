using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Enums;

namespace Zetatech.Accelerate.Data.Abstractions;

public abstract partial class BaseRepository<TEntity> : DbContext, IRepository<TEntity> where TEntity : class, IEntity, new()
{
    private Boolean _disposed;
    private DbSet<TEntity> _entities;
    private readonly ILogger _logger;
    private readonly RepositoryOptions _options;
    private SemaphoreSlim _semaphore;

    protected BaseRepository(IOptions<RepositoryOptions> options,
                             ILoggerFactory loggerFactory)
    {
        _entities = Set<TEntity>();
        _logger = loggerFactory.CreateLogger(GetType().Name);
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _semaphore = new SemaphoreSlim(1, 1);

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

    protected ILogger Logger => _logger;

    public override void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _entities = null;
        _semaphore = null;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TEntity>();
    }
    public override String ToString() => Database.GenerateCreateScript();
}