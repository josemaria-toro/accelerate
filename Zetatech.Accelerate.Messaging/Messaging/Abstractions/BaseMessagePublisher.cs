using System;

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
    public abstract Guid Publish<TBody>(TBody body, String queueName = null) where TBody : class;
}