using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Logging.Entities;

namespace Zetatech.Accelerate.Logging.Repositories;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based repository for traces.
/// </summary>
internal sealed class TracesRepository : PostgreSqlRepository<TraceEntity, PostgreSqlRepositoryOptions>
{
    /// <param name="options">
    /// The configuration options for the repository.
    /// </param>
    public TracesRepository(IOptions<PostgreSqlRepositoryOptions> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">
    /// The builder being used to construct the model for this context.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TraceEntity>()
                    .ToTable("tg_traces", Options.Schema)
                    .HasKey(x => x.Id);

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.AppId)
                    .HasColumnName("cu_app_id")
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Category)
                    .HasColumnName("cs_category")
                    .HasMaxLength(256)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Device)
                    .HasColumnName("cs_device_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Event)
                    .HasColumnName("cs_event_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Id)
                    .HasColumnName("cu_id")
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Message)
                    .HasColumnName("cs_message")
                    .HasMaxLength(8192)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.OperatingSystem)
                    .HasColumnName("cs_os_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.OperationId)
                    .HasColumnName("cu_operation_id")
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Platform)
                    .HasColumnName("cs_platform_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.SeverityLevel)
                    .HasColumnName("cn_severity_level")
                    .IsRequired();

        modelBuilder.Entity<TraceEntity>()
                    .Property(x => x.Timestamp)
                    .HasColumnName("cd_timestamp")
                    .IsRequired();
    }
}