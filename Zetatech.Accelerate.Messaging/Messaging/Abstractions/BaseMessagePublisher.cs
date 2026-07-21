using System;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Messaging.Abstractions;

public abstract class BaseMessagePublisher : IMessagePublisher
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseMessagePublisher(ILoggerFactory loggerFactory)
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
    public abstract Guid Publish<TBody>(TBody body, String queueName = null) where TBody : class;
}