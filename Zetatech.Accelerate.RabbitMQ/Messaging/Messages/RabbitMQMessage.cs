using System;

namespace Zetatech.Accelerate.Messaging.Messages;

public sealed class RabbitMQMessage<TBody> where TBody : class
{
    public TBody Body { get; set; }
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public String W3CTraceSpan { get; set; }
}
