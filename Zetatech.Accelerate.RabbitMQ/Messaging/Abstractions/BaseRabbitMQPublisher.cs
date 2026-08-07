using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Zetatech.Accelerate.Exceptions;
using Zetatech.Accelerate.Messaging.Messages;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseRabbitMQPublisher : BaseMessagePublisher
{
    private readonly IChannel _channel;
    private readonly String _exchangeName;
    private readonly RabbitMQOptions _options;
    private readonly String _queueName;

    protected BaseRabbitMQPublisher(IOptions<RabbitMQOptions> options,
                                    IRabbitMQChannelFactory channelFactory)
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _channel = channelFactory.CreateChannel(_options.ConnectionString,
                                                _options.UseSsl,
                                                _options.SslCertIssuer,
                                                _options.SslCertSerialNumber,
                                                _options.SslCertSubject,
                                                _options.SslCertThumbprint);
        _exchangeName = _options.ExchangeName ?? throw new ConfigurationException("The exchange name has an invalid value", "exchangeName");

        if (!String.IsNullOrEmpty(_options.QueueName))
        {
            _queueName = _options.QueueName;
        }
    }
    public override Guid Publish<TBody>(TBody body, String queueName = null) where TBody : class
    {
        if (body == null)
        {
            throw new ArgumentException("The provided message body must be a valid instance", nameof(body));
        }

        if (String.IsNullOrEmpty(queueName))
        {
            if (String.IsNullOrEmpty(_queueName))
            {
                throw new ArgumentException("The provided queue name has an invalid value", nameof(queueName));
            }

            queueName = _queueName;
        }

        var message = new RabbitMQMessage<TBody>
        {
            Body = body,
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        var activity = Activity.Current;

        if (activity != null)
        {
            var traceflag = activity.ActivityTraceFlags == ActivityTraceFlags.Recorded ? "01" : "00";
            message.W3CTraceSpan = $"00-{activity.TraceId}-{activity.SpanId}-{traceflag}";
        }

        var jsonMessage = Json.ToString(message);
        var messageBuffer = Encoding.UTF8.GetBytes(jsonMessage);
        var publishTask = _channel.BasicPublishAsync(_exchangeName, queueName, messageBuffer);

        if (publishTask.IsFaulted)
        {
            throw new MessagingException($"Error publishing message with id '{message.Id}' to '{queueName}({_exchangeName})'");
        }

        return message.Id;
    }
}