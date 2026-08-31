using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Zetatech.Accelerate.Jobs.Abstractions;

public abstract class BaseJob : BackgroundService
{
    private Boolean _disposed;

    public override void Dispose()
    {
        base.Dispose();

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
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var activity = new Activity(GetType().Name);

        activity.SetIdFormat(ActivityIdFormat.W3C);
        activity.Start();

        try
        {
            await OnExecuteAsync(cancellationToken);
        }
        finally
        {
            activity.Stop();
        }
    }
    protected abstract Task OnExecuteAsync(CancellationToken cancellationToken);
}
