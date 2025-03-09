using Accelerate.Data.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Logging;

/// <summary>
/// Represent a type to perform logging based on file system.
/// </summary>
public sealed class DatabaseLoggerService : LoggerService<DatabaseLoggerServiceOptions>
{
    private const String InsertExceptionQueryString = "INSERT INTO exceptions VALUES (@dateTime, @appName, @categoryName, @logLevel, @eventId, @eventName, @message, @type, @stackTrace);";
    private const String InsertTraceQueryString = "INSERT INTO traces VALUES (@dateTime, @appName, @categoryName, @logLevel, @eventId, @eventName, @message);";

    private readonly IDbConnection _connection;
    private Boolean _disposed;
    private SemaphoreSlim _semaphore;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger service.
    /// </param>
    /// <param name="connection">
    /// Database connection.
    /// </param>
    public DatabaseLoggerService(IOptions<DatabaseLoggerServiceOptions> options, IDbConnection connection) : base(options)
    {
        _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

        if (connection.State == ConnectionState.Broken ||
            connection.State == ConnectionState.Closed)
        {
            throw new InvalidOperationException("Database connection has an invalid state");
        }

        _semaphore = new SemaphoreSlim(1, 1);
    }
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger service.
    /// </param>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    /// <param name="connection">
    /// Database connection.
    /// </param>
    public DatabaseLoggerService(IOptions<DatabaseLoggerServiceOptions> options, IDbConnection connection, String categoryName) : base(options, categoryName)
    {
        _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

        if (connection.State == ConnectionState.Broken ||
            connection.State == ConnectionState.Closed)
        {
            throw new InvalidOperationException("Database connection has an invalid state");
        }

        _semaphore = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        base.Dispose(disposing);

        if (disposing)
        {
            _semaphore.Dispose();
            _semaphore = null;
        }

        _disposed = true;
    }
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of the object to be written.
    /// </typeparam>
    /// <param name="logLevel">
    /// Entry will be written on this level.
    /// </param>
    /// <param name="eventId">
    /// Id of the event.
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
        Task.Run(() =>
        {
            if (IsEnabled(logLevel))
            {
                var message = String.Empty;
                var dateTime = DateTime.UtcNow;

                if (formatter == null)
                {
                    message = $"{state}";
                }
                else
                {
                    message = formatter.Invoke(state, exception);
                }

                _semaphore.Wait();

                try
                {
                    _connection.Insert(InsertTraceQueryString, new Dictionary<String, Object>
                    {
                        { "@appName", Options.AppName },
                        { "@category", CategoryName },
                        { "@dateTime", dateTime },
                        { "@eventId", eventId.Id },
                        { "@eventName", eventId.Name },
                        { "@logLevel", logLevel },
                        { "@message", message }
                    });

                    while (exception != null)
                    {
                        _connection.Insert(InsertExceptionQueryString, new Dictionary<String, Object>
                        {
                            { "@appName", Options.AppName },
                            { "@category", CategoryName },
                            { "@dateTime", dateTime },
                            { "@eventId", eventId.Id },
                            { "@eventName", eventId.Name },
                            { "@logLevel", logLevel },
                            { "@message", exception.Message },
                            { "@stackTrace", exception.StackTrace },
                            { "@type", exception.GetType().Name }
                        });

                        exception = exception.InnerException;
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        });
    }
}