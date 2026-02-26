using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Zetatech.Accelerate.Extensions;
using Zetatech.Accelerate.Infrastructure.Abstractions;

namespace Zetatech.Accelerate.Infrastructure;

internal sealed class Consumer<TBody> : IAsyncBasicConsumer where TBody : class
{
    private IChannel _channel;
    private IConnection _connection;
    private ConnectionFactory _connectionFactory;
    private Boolean _disposed;
    private SubscriberOptions _options;
    private JsonSerializerOptions _serializerOptions;
    private readonly BaseSubscriber<TBody> _subscriber;

    public Consumer(IOptions<SubscriberOptions> options, BaseSubscriber<TBody> subscriber)
    {
        _options = options.Value;
        _connectionFactory = new ConnectionFactory();
        _subscriber = subscriber;
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
    }
    public IChannel Channel => _channel;

    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _connection = null;
        _connectionFactory = null;
        _options = null;
        _serializerOptions = null;
    }
    public async Task HandleBasicCancelAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => { _subscriber.OnSubscriptionBroken(consumerTag); }, cancellationToken);
    }
    public async Task HandleBasicCancelOkAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => { _subscriber.OnSubscriptionClosed(consumerTag); }, cancellationToken);
    }
    public async Task HandleBasicConsumeOkAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => { _subscriber.OnSubscriptionOpened(consumerTag); }, cancellationToken);
    }
    public async Task HandleBasicDeliverAsync(String consumerTag, UInt64 deliveryTag, Boolean redelivered, String exchange, String routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<Byte> body, CancellationToken cancellationToken = default)
    {
        var messageBuffer = body.ToArray();
        var jsonMessage = Encoding.UTF8.GetString(messageBuffer);
        var message = JsonSerializer.Deserialize<Message<TBody>>(jsonMessage, _serializerOptions);

        await Task.Run(() => { _subscriber.OnMessageReceived(consumerTag, message); }, cancellationToken);
    }
    public async Task HandleChannelShutdownAsync(Object channel, ShutdownEventArgs shutdownReason)
    {
        await Task.Run(() => { _subscriber.OnChannelShutdown(); });
    }
    public void Subscribe(String queueName)
    {
        if (_connection == null || !_connection.IsOpen)
        {
            var connectionTask = _connectionFactory.CreateConnectionAsync();

            connectionTask.Wait();

            if (!connectionTask.IsCompletedSuccessfully)
            {
                throw connectionTask.Exception;
            }

            _connection = connectionTask.Result;
        }

        if (_channel == null || _channel.IsClosed)
        {
            var channelTask = _connection.CreateChannelAsync();

            channelTask.Wait();

            if (!channelTask.IsCompletedSuccessfully)
            {
                throw channelTask.Exception;
            }

            _channel = channelTask.Result;
        }

        var consumerTask = _channel.BasicConsumeAsync(arguments: default,
                                                      autoAck: true,
                                                      cancellationToken: default,
                                                      consumer: this,
                                                      consumerTag: queueName,
                                                      exclusive: false,
                                                      noLocal: false,
                                                      queue: queueName);

        consumerTask.Wait();

        if (!consumerTask.IsCompletedSuccessfully)
        {
            throw consumerTask.Exception;
        }
    }
    public void Unsubscribe(String queueName)
    {
        if (_connection != null && _connection.IsOpen)
        {
            if (_channel != null && _channel.IsOpen)
            {
                var channelTask = _channel.BasicCancelAsync(queueName);

                channelTask.Wait();

                if (!channelTask.IsCompletedSuccessfully)
                {
                    throw channelTask.Exception;
                }
            }
        }
    }
}