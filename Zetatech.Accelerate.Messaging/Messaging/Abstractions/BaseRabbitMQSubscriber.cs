using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Zetatech.Accelerate.Exceptions;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseRabbitMQSubscriber<TBody> : BaseMessageSubscriber<TBody>, IAsyncBasicConsumer where TBody : class
{
    private readonly IChannel _channel;
    private readonly RabbitMQOptions _options;
    private readonly String _queueName;

    protected BaseRabbitMQSubscriber(IOptions<RabbitMQOptions> options,
                                     IRabbitMQChannelFactory channelFactory,
                                     ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _channel = channelFactory.CreateChannel(_options.ConnectionString,
                                                _options.UseSsl,
                                                _options.SslCertIssuer,
                                                _options.SslCertSerialNumber,
                                                _options.SslCertSubject,
                                                _options.SslCertThumbprint);
        _queueName = _options.QueueName ?? throw new ConfigurationException("The queue name has an invalid value", "queueName");
    }

    public IChannel Channel => _channel;

    public async Task HandleBasicCancelAsync(String queueName,
                                             CancellationToken cancellationToken = default)
    {
        Logger.LogError($"The suscription for queue '{queueName}' was cancelled");
    }
    public async Task HandleBasicCancelOkAsync(String queueName,
                                               CancellationToken cancellationToken = default)
    {
        Logger.LogInformation($"The suscription for queue '{queueName}' was closed");
    }
    public async Task HandleBasicConsumeOkAsync(String queueName,
                                                CancellationToken cancellationToken = default)
    {
        Logger.LogInformation($"The suscription for queue '{queueName}' was registered successfully");
    }
    public async Task HandleBasicDeliverAsync(String queueName,
                                              UInt64 deliveryTag,
                                              Boolean redelivered,
                                              String exchange,
                                              String routingKey,
                                              IReadOnlyBasicProperties properties,
                                              ReadOnlyMemory<Byte> body,
                                              CancellationToken cancellationToken = default)
    {
        Logger.LogDebug($"New message was received from queue '{queueName}'");

        try
        {
            var activity = new Activity(GetType().Name);
            var messageBuffer = body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(messageBuffer);
            var message = Json.ToObject<RabbitMQMessage<TBody>>(jsonMessage);

            activity.SetIdFormat(ActivityIdFormat.W3C);

            if (!String.IsNullOrEmpty(message.W3CTraceSpan))
            {
                var traceSpanValues = message.W3CTraceSpan.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var traceId = traceSpanValues[1];
                var parentSpanId = traceSpanValues[2];
                var traceFlags = traceSpanValues[3];

                activity.SetParentId(
                    ActivityTraceId.CreateFromString(traceId),
                    ActivitySpanId.CreateFromString(parentSpanId),
                    traceFlags == "00" ? ActivityTraceFlags.None : ActivityTraceFlags.Recorded
                );
            }

            activity.Start();

            try
            {
                OnMessageReceived(message);
            }
            finally
            {
                activity.Stop();
            }

            await _channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }
        catch (JsonException)
        {
            Logger.LogError($"Error deserializating message from queue '{queueName}'");
            await _channel.BasicNackAsync(deliveryTag, false, false, cancellationToken);
        }
        catch (Exception)
        {
            Logger.LogError($"Error processing message from queue '{queueName}'");
            await _channel.BasicNackAsync(deliveryTag, false, !redelivered, cancellationToken);
        }
    }
    public async Task HandleChannelShutdownAsync(Object channel, ShutdownEventArgs shutdownReason)
    {
        Logger.LogInformation($"Shutting down the channel: {shutdownReason.Cause}");
    }
    protected internal abstract void OnMessageReceived(RabbitMQMessage<TBody> message);
    public override void Subscribe()
    {
        Logger.LogDebug($"Suscribing to queue '{_queueName}'");

        var consumerTask = _channel.BasicConsumeAsync(arguments: default,
                                                      autoAck: false,
                                                      cancellationToken: default,
                                                      consumer: this,
                                                      consumerTag: _queueName,
                                                      exclusive: false,
                                                      noLocal: false,
                                                      queue: _queueName);

        consumerTask.Wait();

        if (!consumerTask.IsCompletedSuccessfully)
        {
            throw new MessagingException("Error creating queue message consumer with RabbitMQ server", consumerTask.Exception);
        }
    }
    public override void Unsubscribe()
    {
        Logger.LogDebug($"Unsuscribing from queue '{_queueName}'");

        var cancelTask = _channel.BasicCancelAsync(_queueName);

        cancelTask.Wait();

        if (!cancelTask.IsCompletedSuccessfully)
        {
            throw new MessagingException("Error removing queue message consumer with RabbitMQ server", cancelTask.Exception);
        }
    }
}