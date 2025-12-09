using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zetatech.Accelerate.Domain.Abstractions;
using Zetatech.Accelerate.Infrastructure.Abstractions;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Persistency.Repositories;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based repository.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity managed by the repository. Must inherit from <see cref="BaseEntity"/>.
/// </typeparam>
/// <typeparam name="TOptions">
/// The options for configuring the repository. Must inherit from <see cref="PostgreSqlRepositoryOptions"/>.
/// </typeparam>
public abstract class PostgreSqlRepository<TEntity, TOptions> : BaseRepository<TEntity, TOptions, PostgreSqlRepositoryContext<TEntity, TOptions>> where TEntity : BaseEntity
                                                                                                                                                  where TOptions : PostgreSqlRepositoryOptions
{
    /// <summary>
    /// Initializes a new instance of the class with the specified options.
    /// </summary>
    /// <param name="options">
    /// The repository options to be used.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected PostgreSqlRepository(IOptions<TOptions> options, ITrackingService trackingService = null) : base(options, trackingService)
    {
    }

    /// <summary>
    /// Apply the pending changes in the table schema.
    /// </summary>
    public override async Task ApplyChangesInTableSchemaAsync()
    {
        var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations", $"{typeof(TEntity).GUID}");
        var backupPath = Path.Combine(rootPath, $"{DateTime.UtcNow:yyyyMMdd}");

        if (Directory.Exists(rootPath))
        {
            var sqlFiles = Directory.GetFiles(rootPath, "*.sql", SearchOption.TopDirectoryOnly)
                                    .OrderBy(x => x);

            if (sqlFiles.Any())
            {
                Directory.CreateDirectory(backupPath);
            }

            foreach (var sqlFile in sqlFiles)
            {
                using var streamReader = new StreamReader(sqlFile);
                var sqlSript = streamReader.ReadToEnd();
                await Context.Database.ExecuteSqlRawAsync(sqlSript);
                File.Move(sqlFile, backupPath, true);
            }
        }
    }
    /// <summary>
    /// Execute a custom query string to select entities.
    /// </summary>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    public override async Task<IQueryable<TEntity>> ExecuteAsync(String queryString, params IDbDataParameter[] parameters)
    {
        if (String.IsNullOrEmpty(queryString))
        {
            throw new ArgumentException("The query string to execute cannot be null or empty", nameof(queryString));
        }

        return Entities.FromSqlRaw(queryString, parameters);
    }
}