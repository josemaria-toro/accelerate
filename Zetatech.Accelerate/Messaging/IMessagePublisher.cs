using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

public interface IMessagePublisher : IDisposable
{
    Guid Publish<TBody>(TBody body,
                        String queueName = null) where TBody : class;
    Task<Guid> PublishAsync<TBody>(TBody body,
                                   String queueName = null,
                                   CancellationToken cancellationToken = default) where TBody : class;
}
