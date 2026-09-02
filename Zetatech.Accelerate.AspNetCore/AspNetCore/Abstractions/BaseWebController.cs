using System;
using Microsoft.AspNetCore.Mvc;

namespace Zetatech.Accelerate.AspNetCore.Abstractions;

public abstract class BaseWebController : Controller
{
    private Boolean _disposed;

    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose(disposing);
    }
}
