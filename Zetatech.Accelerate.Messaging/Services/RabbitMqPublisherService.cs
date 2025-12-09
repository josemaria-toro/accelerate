using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zetatech.Accelerate.Messaging.Abstractions;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Messaging.Services;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based message publisher.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be published.
/// </typeparam>
/// <typeparam name="TOptions">
/// The options for configuring the message publisher. Must inherit from <see cref="RabbitMqPublisherServiceOptions"/>.
/// </typeparam>
public abstract class RabbitMqPublisherService<TMessage, TOptions> : BasePublisherService<TMessage, TOptions> where TMessage : BaseMessage
                                                                                                              where TOptions : RabbitMqPublisherServiceOptions
{
    private IChannel _channel;
    private IConnection _connection;
    private ConnectionFactory _connectionFactory;
    private Boolean _disposed;
    private JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages publisher.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    /// <param name="jsonSerializerOptions">
    /// Options for objects serializing and deserializing.
    /// </param>
    public RabbitMqPublisherService(IOptions<TOptions> options, ITrackingService trackingService = null, JsonSerializerOptions jsonSerializerOptions = null) : base(options, trackingService)
    {
        _jsonSerializerOptions = jsonSerializerOptions;

        if (_jsonSerializerOptions == null)
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                AllowDuplicateProperties = false,
                AllowTrailingCommas = false,
                DefaultBufferSize = 4096,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false,
                IncludeFields = false,
                MaxDepth = 64,
                NumberHandling = JsonNumberHandling.Strict,
                PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                WriteIndented = true
            };

            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        _connectionFactory = new ConnectionFactory();

        var sections = Options.ConnectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var parameters in sections)
        {
            var parameter = parameters.Split("=", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (parameter[0].Equals("host", StringComparison.InvariantCultureIgnoreCase))
            {
                _connectionFactory.HostName = parameter[1];
            }
            else if (parameter[0].Equals("pass", StringComparison.InvariantCultureIgnoreCase))
            {
                _connectionFactory.Password = parameter[1];
            }
            else if (parameter[0].Equals("port", StringComparison.InvariantCultureIgnoreCase))
            {
                _connectionFactory.Port = Int32.Parse(parameter[1]);
            }
            else if (parameter[0].Equals("user", StringComparison.InvariantCultureIgnoreCase))
            {
                _connectionFactory.UserName = parameter[1];
            }
            else if (parameter[0].Equals("vhost", StringComparison.InvariantCultureIgnoreCase))
            {
                _connectionFactory.VirtualHost = parameter[1];
            }
        }

        var connectionTask = _connectionFactory.CreateConnectionAsync();

        connectionTask.Wait();

        if (connectionTask.IsCompletedSuccessfully)
        {
            _connection = connectionTask.Result;
        }
        else
        {
            throw connectionTask.Exception;
        }

        var channelTask = _connection.CreateChannelAsync();

        channelTask.Wait();

        if (channelTask.IsCompletedSuccessfully)
        {
            _channel = channelTask.Result;
        }
        else
        {
            throw channelTask.Exception;
        }
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

        base.Dispose(disposing);

        if (disposing)
        {
            _channel = null;
            _connection = null;
            _connectionFactory = null;
        }
    }
    /// <summary>
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    public override async Task PublishAsync(TMessage message)
    {
        if (message.Id == Guid.Empty)
        {
            message.Id = Guid.NewGuid();
        }

        if (message.OperationId == Guid.Empty)
        {
            message.OperationId = message.Id;
        }

        message.Timestamp = DateTime.UtcNow;

        var jsonMessage = JsonSerializer.Serialize(message, _jsonSerializerOptions);
        var jsonMessageBuffer = Encoding.UTF8.GetBytes(jsonMessage);

        await _channel.BasicPublishAsync(Options.Exchange, Options.RoutingKey, jsonMessageBuffer);
    }
}
