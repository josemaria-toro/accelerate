using System;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Domain.Abstractions;

public abstract class BaseDomainService : IDomainService
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseDomainService(ILoggerFactory loggerFactory)
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
}