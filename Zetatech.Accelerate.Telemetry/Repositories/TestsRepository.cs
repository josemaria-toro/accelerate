using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Telemetry.Entities;

namespace Zetatech.Accelerate.Telemetry.Repositories;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based repository for tests.
/// </summary>
internal sealed class TestsRepository : PostgreSqlRepository<TestEntity, PostgreSqlRepositoryOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the repository.
    /// </param>
    public TestsRepository(IOptions<PostgreSqlRepositoryOptions> options) : base(options)
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
        modelBuilder.Entity<TestEntity>()
                    .ToTable("tg_tests", Options.Schema)
                    .HasKey(x => x.Id);

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.AppId)
                    .HasColumnName("cu_app_id")
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Duration)
                    .HasColumnName("cn_duration")
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Id)
                    .HasColumnName("cu_id")
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Message)
                    .HasColumnName("cs_message")
                    .HasMaxLength(8192)
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Name)
                    .HasColumnName("cs_name")
                    .HasMaxLength(128)
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.OperationId)
                    .HasColumnName("cu_operation_id")
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Success)
                    .HasColumnName("cb_success")
                    .IsRequired();

        modelBuilder.Entity<TestEntity>()
                    .Property(x => x.Timestamp)
                    .HasColumnName("cd_timestamp")
                    .IsRequired();
    }
}