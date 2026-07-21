using System;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Data;

namespace Zetatech.Accelerate.Domain.Abstractions;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class, IEntity, new()
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseSpecification(ILoggerFactory loggerFactory)
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
    public abstract Boolean IsSatisfiedBy(TEntity entity);
}