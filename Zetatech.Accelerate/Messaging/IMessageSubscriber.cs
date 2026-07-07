using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

public interface IMessageSubscriber<TBody> : IDisposable where TBody : class
{
    void Subscribe();
    Task SubscribeAsync(CancellationToken cancellationToken = default);
    void Unsubscribe();
    Task UnsubscribeAsync(CancellationToken cancellationToken = default);
}