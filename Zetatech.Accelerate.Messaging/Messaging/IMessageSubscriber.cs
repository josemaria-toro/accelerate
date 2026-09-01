using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

public interface IMessageSubscriber<TBody> : IDisposable where TBody : class
{
    Task SubscribeAsync(CancellationToken cancellationToken = default);
    Task UnsubscribeAsync(CancellationToken cancellationToken = default);
}