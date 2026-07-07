using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseMessageSubscriber<TBody> : IMessageSubscriber<TBody> where TBody : class
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseMessageSubscriber(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType().Name);
    }

    protected ILogger Logger => _logger;

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
    public abstract void Subscribe();
    public async Task SubscribeAsync(CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Subscribe(), cancellationToken);
    }
    public abstract void Unsubscribe();
    public async Task UnsubscribeAsync(CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Subscribe(), cancellationToken);
    }
}