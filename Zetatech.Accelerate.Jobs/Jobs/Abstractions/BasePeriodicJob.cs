using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Jobs.Abstractions;

public abstract class BasePeriodicJob : BaseJob
{
    private Boolean _disposed;
    private Boolean _runOnStartup;
    private PeriodicTimer _timer;

    protected BasePeriodicJob(TimeSpan interval,
                              Boolean runOnStartup = false)
    {
        _runOnStartup = runOnStartup;
        _timer = new PeriodicTimer(interval);
    }

    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose(disposing);

        if (disposing)
        {
            _timer = null;
        }
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (_runOnStartup || await _timer.WaitForNextTickAsync(cancellationToken))
            {
                _runOnStartup = false;

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
        finally
        {
            _timer.Dispose();
        }
    }
}
