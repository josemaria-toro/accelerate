using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Zetatech.Accelerate.Jobs.Abstractions;

public abstract class BaseTimerJob : BackgroundService, ITimerJob
{
    private Boolean _disposed;
    private PeriodicTimer _timer;

    protected BaseTimerJob(TimeSpan interval)
    {
        _timer = new PeriodicTimer(interval);
    }

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

        if (disposing)
        {
            _timer = null;
        }
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(cancellationToken))
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
        }
        catch (OperationCanceledException)
        {
            _timer.Dispose();
        }
    }
    protected abstract Task OnExecuteAsync(CancellationToken cancellationToken);
}
