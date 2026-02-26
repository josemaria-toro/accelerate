using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Zetatech.Accelerate.Application;
using Zetatech.Accelerate.Extensions;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class Publisher : IPublisher
{
    private IChannel _channel;
    private IConnection _connection;
    private ConnectionFactory _connectionFactory;
    private Boolean _disposed;
    private PublisherOptions _options;
    private JsonSerializerOptions _serializerOptions;

    public Publisher(IOptions<PublisherOptions> options)
    {
        _options = options.Value;
        _connectionFactory = new ConnectionFactory();
        _serializerOptions = JsonSerializerOptions.Default.GetDefaultOptions();

        var sections = _options.ConnectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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

    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _channel = null;
        _connection = null;
        _connectionFactory = null;
        _options = null;
        _serializerOptions = null;

        GC.SuppressFinalize(this);
    }
    public void Publish<TBody>(TBody body, String queueName) where TBody : class
    {
        var message = new Message<TBody>
        {
            Body = body,
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        var serializedMessage = JsonSerializer.Serialize(message, _serializerOptions);
        var messageBuffer = Encoding.UTF8.GetBytes(serializedMessage);

        _channel.BasicPublishAsync(_options.Exchange, queueName, messageBuffer);
    }
}