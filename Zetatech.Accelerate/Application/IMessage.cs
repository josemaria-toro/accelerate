using System;

namespace Zetatech.Accelerate.Application;

public interface IMessage<TBody> where TBody : class
{
    TBody Body { get; set; }
    Guid Id { get; set; }
    DateTime Timestamp { get; set; }
}
