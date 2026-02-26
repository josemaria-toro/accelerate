using System;

namespace Zetatech.Accelerate.Application.Abstractions;

public abstract class BaseService : IService
{
    private Boolean _disposed;
    private readonly ITracker _tracker;

    protected BaseService(ITracker tracker = null)
    {
        _tracker = tracker;
    }

    protected ITracker Tracker => _tracker;

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