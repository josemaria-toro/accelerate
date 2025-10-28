using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Data.Repositories;
using Zetatech.Accelerate.Logging.Entities;
using Zetatech.Accelerate.Logging.Repositories;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Services;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based logging service.
/// </summary>
public sealed class PostgreSqlLoggingService : BaseLoggingService<PostgreSqlLoggingServiceOptions>
{
    private Boolean _disposed;
    private IRepository<ErrorEntity> _errorsRepository;
    private IRepository<TraceEntity> _tracesRepository;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the logging service.
    /// </param>
    /// <param name="categoryName">
    /// The category name for the logger.
    /// </param>
    public PostgreSqlLoggingService(IOptions<PostgreSqlLoggingServiceOptions> options, String categoryName) : base(options, categoryName)
    {
        var postgreSqlRepositoryOptions = new PostgreSqlRepositoryOptions
        {
            ConnectionString = Options.ConnectionString,
            DetailedErrors = Options.DetailedErrors,
            LazyLoading = Options.LazyLoading,
            Schema = Options.Schema,
            SensitiveDataLogging = Options.SensitiveDataLogging,
            Timeout = Options.Timeout,
            TrackChanges = Options.TrackChanges
        };

        _errorsRepository = new ErrorsRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions));
        _tracesRepository = new TracesRepository(OptionsBuilder.Create(postgreSqlRepositoryOptions));
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose();

        if (disposing)
        {
            _errorsRepository = null;
            _tracesRepository = null;
        }
    }
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeParam name="TState">
    /// The type of the object to be written.
    /// </typeParam>
    /// <param name="logLevel">
    /// Entry will be written on this level.
    /// </param>
    /// <param name="eventId">
    /// The event id associated with the log.
    /// </param>
    /// <param name="state">
    /// The entry to be written.
    /// </param>
    /// <param name="exception">
    /// The exception related to this entry.
    /// </param>
    /// <param name="formatter">
    /// Function to create a string message of the state and exception.
    /// </param>
    public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
    {
        if (IsEnabled(logLevel))
        {
            var operationId = Guid.NewGuid();

            _tracesRepository.Insert(new TraceEntity
            {
                AppId = Options.AppId,
                Category = Category,
                Device = Environment.MachineName,
                Event = eventId.Name ?? String.Empty,
                Id = Guid.NewGuid(),
                Message = $"{state}",
                OperatingSystem = Environment.OSVersion.VersionString,
                OperationId = operationId,
                Platform = Platform,
                SeverityLevel = (Int32)logLevel,
                Timestamp = DateTime.UtcNow
            });

            _tracesRepository.Commit();

            if (exception != null)
            {
                while (exception != null)
                {
                    _errorsRepository.Insert(new ErrorEntity
                    {
                        AppId = Options.AppId,
                        Category = Category,
                        Device = Environment.MachineName,
                        Event = eventId.Name ?? String.Empty,
                        Id = Guid.NewGuid(),
                        Message = exception.Message,
                        OperatingSystem = Environment.OSVersion.VersionString,
                        OperationId = operationId,
                        Platform = Platform,
                        SeverityLevel = (Int32)logLevel,
                        StackTrace = exception.StackTrace,
                        Timestamp = DateTime.UtcNow,
                        TypeName = exception.GetType().Name
                    });

                    exception = exception.InnerException;
                }

                _errorsRepository.Commit();
            }
        }
    }
}