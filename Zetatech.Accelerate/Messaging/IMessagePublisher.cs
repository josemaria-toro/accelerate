using System;

namespace Zetatech.Accelerate.Messaging;

public interface IMessagePublisher : IDisposable
{
    Guid Publish<TBody>(TBody body,
                        String queueName = null) where TBody : class;
}
