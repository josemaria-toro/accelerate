using System;

namespace Zetatech.Accelerate.Domain.Abstractions;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
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
    public abstract Boolean IsSatisfiedBy(TEntity entity);
}