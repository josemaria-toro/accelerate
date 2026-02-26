using System;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Application;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

public abstract class BaseSubscriber<TBody> : ISubscriber<TBody> where TBody : class
{
    private Consumer<TBody> _consumer;
    private Boolean _disposed;

    protected BaseSubscriber(IOptions<SubscriberOptions> options)
    {
        _consumer = new Consumer<TBody>(options, this);
    }

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

        if (disposing)
        {
            _consumer = null;
        }
    }
    protected internal virtual void OnChannelShutdown() { }
    protected internal abstract void OnMessageReceived(String queueName, IMessage<TBody> message);
    protected internal virtual void OnSubscriptionBroken(String queueName) { }
    protected internal virtual void OnSubscriptionClosed(String queueName) { }
    protected internal virtual void OnSubscriptionOpened(String queueName) { }
    public void Subscribe(String queueName)
    {
        _consumer.Subscribe(queueName);
    }
    public void Unsubscribe(String queueName)
    {
        _consumer.Unsubscribe(queueName);
    }
}