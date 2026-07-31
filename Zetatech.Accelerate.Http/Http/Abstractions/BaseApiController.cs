using System;
using Microsoft.AspNetCore.Mvc;

namespace Zetatech.Accelerate.Http.Abstractions;

[ApiController]
public abstract class BaseApiController : ControllerBase, IDisposable
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
}
