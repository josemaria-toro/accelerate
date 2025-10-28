using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Messaging.Subscribers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based message subscriber.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be published.
/// </typeparam>
/// <typeparam name="TOptions">
/// The options for configuring the message subscriber. Must inherit from <see cref="RabbitMqSubscriberServiceOptions"/>.
/// </typeparam>
public abstract class RabbitMqSubscriberService<TMessage, TOptions> : BaseSubscriberService<TMessage, TOptions>, IAsyncBasicConsumer where TMessage : BaseMessage
                                                                                                                                     where TOptions : RabbitMqSubscriberServiceOptions
{
    private IChannel _channel;
    private IConnection _connection;
    private ConnectionFactory _connectionFactory;
    private Boolean _disposed;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages subscriber.
    /// </param>
    public RabbitMqSubscriberService(IOptions<TOptions> options) : base(options)
    {
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

        var suscriptionTask = _channel.BasicConsumeAsync(arguments: default,
                                                         autoAck: true,
                                                         cancellationToken: default,
                                                         consumer: this,
                                                         consumerTag: GetType().Name,
                                                         exclusive: false,
                                                         noLocal: false,
                                                         queue: Options.Queue);

        suscriptionTask.Wait();

        if (!suscriptionTask.IsCompletedSuccessfully)
        {
            throw suscriptionTask.Exception;
        }
    }

    /// <summary>
    /// Gets the channel of this consumer is associated with, for use in acknowledging received messages, for instance.
    /// </summary>
    public IChannel Channel => _channel;

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
    /// Called when the consumer is cancelled for reasons other than by a basicCancel.
    /// </summary>
    /// <param name="consumerTag">
    /// Consumer tag of this consumer.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task HandleBasicCancelAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        Logger.LogWarning("The consumer was cancelled for unknown reasons");

        await Task.FromResult(0);
    }
    /// <summary>
    /// Called when the unsuscription from the broker was done successful.
    /// </summary>
    /// <param name="consumerTag">
    /// Consumer tag of this consumer.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task HandleBasicCancelOkAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("The unsuscription from the message broker was done successful");

        await Task.FromResult(0);
    }
    /// <summary>
    /// Called when the suscription from the broker was done successful.
    /// </summary>
    /// <param name="consumerTag">
    /// Consumer tag of this consumer.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task HandleBasicConsumeOkAsync(String consumerTag, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("The suscription from the message broker was done successfully");

        await Task.FromResult(0);
    }
    /// <summary>
    /// Called each time a message arrives for this consumer.
    ///</summary>
    /// <param name="consumerTag">
    /// Consumer tag of this consumer.
    /// </param>
    /// <param name="deliveryTag">
    /// Tag to identify the delivery.
    /// </param>
    /// <param name="redelivered">
    /// Flag that indicates if the message was redelivered due an error.
    /// </param>
    /// <param name="exchange">
    /// The exchange name used to consume the message.
    /// </param>
    /// <param name="routingKey">
    /// The routing key used to consume the message.
    /// </param>
    /// <param name="properties">
    /// The additional properties used by the queue.
    /// </param>
    /// <param name="body">
    /// The body of the message object.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task HandleBasicDeliverAsync(String consumerTag, UInt64 deliveryTag, Boolean redelivered, String exchange, String routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<Byte> body, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("New message was received from the message broker");

        var jsonMessageBuffer = body.ToArray();
        var jsonMessage = Encoding.UTF8.GetString(jsonMessageBuffer);
        var message = JsonSerializer.Deserialize<TMessage>(jsonMessage);

        await Task.Run(() => { Receive(message); }, cancellationToken);
    }
    /// <summary>
    /// Called when the channel shuts down.
    /// </summary>
    /// <param name="channel">
    /// Common AMQP channel.
    /// </param>
    /// <param name="shutdownReason">
    /// Information about the reason why a particular channel, session, or connection was destroyed.
    /// </param>
    public async Task HandleChannelShutdownAsync(Object channel, ShutdownEventArgs shutdownReason)
    {
        Logger.LogDebug($"The AMQP channel was shutdown by {shutdownReason.Cause}");

        await Task.FromResult(0);
    }
}
