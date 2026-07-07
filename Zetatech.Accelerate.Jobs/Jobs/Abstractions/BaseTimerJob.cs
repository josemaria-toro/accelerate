using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Jobs.Abstractions;

public abstract class BaseTimerJob : BackgroundService, ITimerJob
{
    private Boolean _disposed;
    private ILogger _logger;
    private PeriodicTimer _timer;

    protected BaseTimerJob(TimeSpan interval, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType().Name);
        _timer = new PeriodicTimer(interval);
    }

    protected ILogger Logger => _logger;

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
            _logger = null;
            _timer = null;
        }
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timer job started");

        try
        {
            while (await _timer.WaitForNextTickAsync(cancellationToken))
            {
                var activity = new Activity(GetType().Name);

                activity.SetIdFormat(ActivityIdFormat.W3C);
                activity.Start();

                _logger.LogDebug("New execution of timer job");

                try
                {
                    Execute();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing the timer job");
                }
                finally
                {
                    activity.Stop();
                }

                _logger.LogDebug($"The duration of the execution was {activity.Duration.TotalSeconds:0.00000} seconds");
            }
        }
        catch (OperationCanceledException)
        {
            _timer.Dispose();
        }

        _logger.LogInformation("Timer job stopped");
    }
    protected abstract void Execute();
}
