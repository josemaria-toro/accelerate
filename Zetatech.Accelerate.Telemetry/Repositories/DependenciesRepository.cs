using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Telemetry.Entities;

namespace Zetatech.Accelerate.Telemetry.Repositories;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based repository for dependencies.
/// </summary>
internal sealed class DependenciesRepository : PostgreSqlRepository<DependencyEntity, PostgreSqlRepositoryOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the repository.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    public DependenciesRepository(IOptions<PostgreSqlRepositoryOptions> options, ILoggerFactory loggerFactory = null) : base(options, loggerFactory)
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
        modelBuilder.Entity<DependencyEntity>()
                    .ToTable("tg_dependencies", Options.Schema)
                    .HasKey(x => x.Id);

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.AppId)
                    .HasColumnName("cu_app_id")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.Duration)
                    .HasColumnName("cn_duration")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.Id)
                    .HasColumnName("cu_id")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.InputData)
                    .HasColumnName("cs_input_data")
                    .HasMaxLength(8192)
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.Name)
                    .HasColumnName("cs_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.OperationId)
                    .HasColumnName("cu_operation_id")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.OutputData)
                    .HasColumnName("cs_output_data")
                    .HasMaxLength(8192)
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.Success)
                    .HasColumnName("cb_success")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.OutputData)
                    .HasColumnName("cs_target")
                    .HasMaxLength(4096)
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.Timestamp)
                    .HasColumnName("cd_timestamp")
                    .IsRequired();

        modelBuilder.Entity<DependencyEntity>()
                    .Property(x => x.OutputData)
                    .HasColumnName("cs_type")
                    .HasMaxLength(128)
                    .IsRequired();
    }
}