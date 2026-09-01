using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseMessagePublisher : IMessagePublisher
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
    public abstract Task<Guid> PublishAsync<TBody>(TBody body,
                                                   String queueName = null,
                                                   CancellationToken cancellationToken = default) where TBody : class;
}