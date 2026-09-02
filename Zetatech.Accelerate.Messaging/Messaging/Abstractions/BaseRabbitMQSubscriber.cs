using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Zetatech.Accelerate.Exceptions;
using Zetatech.Accelerate.Messaging.Factories;
using Zetatech.Accelerate.Messaging.Messages;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseRabbitMQSubscriber<TBody> : BaseMessageSubscriber<TBody>, IAsyncBasicConsumer where TBody : class
{
    private IChannel _channel;
    private readonly RabbitMQOptions _options;
    private readonly String _queueName;

    protected BaseRabbitMQSubscriber(IOptions<RabbitMQOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _queueName = _options.QueueName ?? throw new ConfigurationException("The queue name has an invalid value", "queueName");
    }

    public IChannel Channel
    {
        get
        {
            if (_channel == null)
            {
                var channelTask = RabbitMQChannelFactory.Current.CreateChannelAsync(_options);
                channelTask.Wait();
                _channel = channelTask.Result;
            }

            return _channel;
        }
    }

    public virtual async Task HandleBasicCancelAsync(String queueName, CancellationToken cancellationToken = default) => cancellationToken.ThrowIfCancellationRequested();
    public virtual async Task HandleBasicCancelOkAsync(String queueName, CancellationToken cancellationToken = default) => cancellationToken.ThrowIfCancellationRequested();
    public virtual async Task HandleBasicConsumeOkAsync(String queueName, CancellationToken cancellationToken = default) => cancellationToken.ThrowIfCancellationRequested();
    public async Task HandleBasicDeliverAsync(String queueName, UInt64 deliveryTag, Boolean redelivered, String exchange, String routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<Byte> body, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

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
                await OnMessageReceivedAsync(message, cancellationToken);
            }
            finally
            {
                activity.Stop();
            }

            await _channel.BasicAckAsync(deliveryTag, false, cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (JsonException)
        {
            await _channel.BasicNackAsync(deliveryTag, false, false, cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await _channel.BasicNackAsync(deliveryTag, false, !redelivered, cancellationToken)
                          .ConfigureAwait(false);
        }
    }
    public virtual async Task HandleChannelShutdownAsync(Object channel, ShutdownEventArgs shutdownReason)
    {
    }
    protected abstract Task OnMessageReceivedAsync(RabbitMQMessage<TBody> message, CancellationToken cancellationToken = default);
    public override async Task SubscribeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _channel.BasicConsumeAsync(_queueName, false, _queueName, false, false, default, this, cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new MessagingException("Error creating queue message consumer with RabbitMQ server", ex);
        }
    }
    public override async Task UnsubscribeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _channel.BasicCancelAsync(_queueName, false, cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new MessagingException("Error removing queue message consumer with RabbitMQ server", ex);
        }
    }
}
