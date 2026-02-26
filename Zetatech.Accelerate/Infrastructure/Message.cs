using System;
using Zetatech.Accelerate.Application;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class Message<TBody> : IMessage<TBody> where TBody : class
{
    public TBody Body { get; set; }
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
}
