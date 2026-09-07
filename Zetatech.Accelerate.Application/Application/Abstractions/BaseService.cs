using System;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Application.Abstractions;

public abstract class BaseService : IService
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseService(ILoggerFactory loggerFactory = null)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
    }

    public ILogger Logger => _logger;

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