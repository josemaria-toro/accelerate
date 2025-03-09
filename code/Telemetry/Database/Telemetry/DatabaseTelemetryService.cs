using Accelerate.Data.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Telemetry;

/// <summary>
/// Telemetry service based on database.
/// </summary>
public sealed class DatabaseTelemetryService : TelemetryService<DatabaseTelemetryServiceOptions>
{
    private const String InsertAvailabilityQueryString = "INSERT INTO availability VALUES (@appName, @correlationId, @id, @message, @metadata, @name, @success, @timestamp);";
    private const String InsertDependencyQueryString = "INSERT INTO dependencies VALUES (@appName, @correlationId, @data, @duration, @id, @metadata, @name, @resultCode, @success, @target, @timestamp, @type);";
    private const String InsertEventQueryString = "INSERT INTO events VALUES (@appName, @correlationId, @id, @metadata, @name, @timestamp);";
    private const String InsertMetricQueryString = "INSERT INTO metrics VALUES (@appName, @correlationId, @dimension, @id, @name, @timestamp, @value);";
    private const String InsertPageViewQueryString = "INSERT INTO pages VALUES (@appName, @correlationId, @duration, @id, @metadata, @name, @timestamp, @uri);";
    private const String InsertRequestQueryString = "INSERT INTO requests VALUES (@appName, @client, @correlationId, @duration, @id, @metadata, @name, @responseCode, @timestamp, @uri);";

    private readonly IDbConnection _connection;
    private Boolean _disposed;
    private JsonSerializerOptions _jsonSerializerOptions;
    private SemaphoreSlim _semaphore;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options.
    /// </param>
    /// <param name="connection">
    /// Database connection.
    /// </param>
    public DatabaseTelemetryService(IOptions<DatabaseTelemetryServiceOptions> options, IDbConnection connection) : base(options)
    {
        _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

        if (connection.State == ConnectionState.Broken ||
            connection.State == ConnectionState.Closed)
        {
            throw new InvalidOperationException("Database connection has an invalid state");
        }

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = false,
            MaxDepth = 32,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
            WriteIndented = false
        };

        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

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
            _jsonSerializerOptions = null;
            _semaphore.Dispose();
            _semaphore = null;
        }

        _disposed = true;
    }
    /// <summary>
    /// Track the availablity information of a service or application.
    /// </summary>
    /// <param name="availability">
    /// Service or application availability information.
    /// </param>
    public override void Track(Availability availability)
    {
        if (availability == null)
        {
            throw new ArgumentException("Availability information cannot be null", nameof(availability));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertAvailabilityQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@correlationId", availability.CorrelationId },
                    { "@id", Guid.NewGuid() },
                    { "@message", availability.Message },
                    { "@metadata", JsonSerializer.Serialize(availability.Metadata, _jsonSerializerOptions) },
                    { "@name", availability.Name },
                    { "@success", availability.Success },
                    { "@timestamp", availability.Timestamp.ToUniversalTime() }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
    /// <summary>
    /// Track the dependency information of a service or application.
    /// </summary>
    /// <param name="dependency">
    /// Service or application dependency information.
    /// </param>
    public override void Track(Dependency dependency)
    {
        if (dependency == null)
        {
            throw new ArgumentException("Dependency information cannot be null", nameof(dependency));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertDependencyQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@correlationId", dependency.CorrelationId },
                    { "@data", dependency.Data },
                    { "@duration", dependency.Duration.TotalMilliseconds },
                    { "@id", Guid.NewGuid() },
                    { "@metadata", JsonSerializer.Serialize(dependency.Metadata, _jsonSerializerOptions) },
                    { "@name", dependency.Name },
                    { "@resultCode", dependency.Results },
                    { "@success", dependency.Success },
                    { "@target", dependency.Target },
                    { "@timestamp", dependency.Timestamp.ToUniversalTime() },
                    { "@type", dependency.Type }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
    /// <summary>
    /// Track a service or application event.
    /// </summary>
    /// <param name="event">
    /// Service or application event information.
    /// </param>
    public override void Track(Event @event)
    {
        if (@event == null)
        {
            throw new ArgumentException("Event information cannot be null", nameof(@event));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertDependencyQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@correlationId", @event.CorrelationId },
                    { "@id", Guid.NewGuid() },
                    { "@metadata", JsonSerializer.Serialize(@event.Metadata, _jsonSerializerOptions) },
                    { "@name", @event.Name },
                    { "@timestamp", @event.Timestamp.ToUniversalTime() }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
    /// <summary>
    /// Track a service or application metric.
    /// </summary>
    /// <param name="metric">
    /// Service or application metric information.
    /// </param>
    public override void Track(Metric metric)
    {
        if (metric == null)
        {
            throw new ArgumentException("Metric information cannot be null", nameof(metric));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertMetricQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@correlationId", metric.CorrelationId },
                    { "@dimension", metric.Dimension },
                    { "@id", Guid.NewGuid() },
                    { "@name", metric.Name },
                    { "@timestamp", DateTime.UtcNow },
                    { "@value", metric.Value }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
    /// <summary>
    /// Track a page view of a user.
    /// </summary>
    /// <param name="pageView">
    /// Page information.
    /// </param>
    public override void Track(PageView pageView)
    {
        if (pageView == null)
        {
            throw new ArgumentException("Page view information cannot be null", nameof(pageView));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertPageViewQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@correlationId", pageView.CorrelationId },
                    { "@duration", pageView.Duration.TotalMilliseconds },
                    { "@id", Guid.NewGuid() },
                    { "@metadata", JsonSerializer.Serialize(pageView.Metadata, _jsonSerializerOptions) },
                    { "@name", pageView.Name },
                    { "@timestamp", pageView.Timestamp.ToUniversalTime() },
                    { "@uri", pageView.Uri }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
    /// <summary>
    /// Track an http request information.
    /// </summary>
    /// <param name="request">
    /// Http request information.
    /// </param>
    public override void Track(Request request)
    {
        if (request == null)
        {
            throw new ArgumentException("Request information cannot be null", nameof(request));
        }

        Task.Run(async () =>
        {
            await _semaphore.WaitAsync();

            try
            {
                _connection.Insert(InsertRequestQueryString, new Dictionary<String, Object>
                {
                    { "@appName", Options.AppName },
                    { "@client", request.Client },
                    { "@correlationId", request.CorrelationId },
                    { "@duration", request.Duration.TotalMilliseconds },
                    { "@id", Guid.NewGuid() },
                    { "@metadata", JsonSerializer.Serialize(request.Metadata, _jsonSerializerOptions) },
                    { "@name", request.Name },
                    { "@responseCode", request.ResponseCode },
                    { "@timestamp", request.Timestamp.ToUniversalTime() },
                    { "@uri", request.Uri }
                });
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }
}