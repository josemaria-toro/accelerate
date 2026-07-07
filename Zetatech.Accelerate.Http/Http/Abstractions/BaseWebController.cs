using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Http.Abstractions;

public abstract class BaseWebController : Controller
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseWebController(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType().Name);
    }

    protected ILogger Logger => _logger;

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
