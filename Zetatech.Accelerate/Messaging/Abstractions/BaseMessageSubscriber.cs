using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseMessageSubscriber<TBody> : IMessageSubscriber<TBody> where TBody : class
{
    private Boolean _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
    }
    public abstract Task SubscribeAsync(CancellationToken cancellationToken = default);
    public abstract Task UnsubscribeAsync(CancellationToken cancellationToken = default);
}