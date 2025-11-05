using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Zetatech.Accelerate.Data.Repositories;

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
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    protected PostgreSqlRepository(IOptions<TOptions> options, ILoggerFactory loggerFactory = null) : base(options, loggerFactory)
    {
    }

    /// <summary>
    /// Apply the pending changes in the table schema.
    /// </summary>
    public override void ApplyChangesInTableSchema()
    {
        var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "migrations", $"{typeof(TEntity).GUID}");
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
                Context.Database.ExecuteSqlRaw(sqlSript);
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
    /// <returns>
    /// The list of entities selected.
    /// </returns>
    public override IQueryable<TEntity> Select(String queryString, params IDbDataParameter[] parameters)
    {
        Logger?.LogDebug("Selecting a collection of entities using a query string");

        if (String.IsNullOrEmpty(queryString))
        {
            throw new ArgumentException("The query string to execute cannot be null or empty", nameof(queryString));
        }

        return Entities.FromSqlRaw(queryString, parameters);
    }
}