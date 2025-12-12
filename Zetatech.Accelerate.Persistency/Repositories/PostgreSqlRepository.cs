using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    public override async Task ApplyChangesInTableSchemaAsync(Guid operationId)
    {
        Context.Database.EnsureCreated();

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
    /// <param name="operationId">
    /// The operation identifier used to track the activity.
    /// </param>
    /// <param name="queryString">
    /// The query string to execute.
    /// </param>
    /// <param name="parameters">
    /// The values to be assigned to parameters.
    /// </param>
    public override async Task<IEnumerable<TEntity>> ExecuteAsync(Guid operationId, String queryString, params IDbDataParameter[] parameters)
    {
        if (String.IsNullOrEmpty(queryString))
        {
            throw new ArgumentException("The query string to execute cannot be null or empty", nameof(queryString));
        }

        var utcnow = DateTime.UtcNow;
        var dependency = new Dependency
        {
            Name = "Execute",
            OperationId = operationId,
            TargetName = Context.GetDataSource(),
            Type = "SQL"
        };

        try
        {
            var queryExpression = Entities.FromSqlRaw(queryString, parameters);
            dependency.InputData = queryExpression.ToQueryString();
            var entities = await queryExpression.ToListAsync();
            dependency.OutputData = $"{String.Join(',', entities.Select(x => x.Id))}";
            dependency.Success = true;

            return entities;
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting the entity into the data store", ex);
        }
        finally
        {
            dependency.Duration = DateTime.UtcNow - utcnow;
            await TrackingService?.TrackAsync(dependency);
        }
    }
}