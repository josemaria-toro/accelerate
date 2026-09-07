using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Jobs.Abstractions;

public abstract class BaseJob : BackgroundService
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseJob(ILoggerFactory loggerFactory = null)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
    }

    public ILogger Logger => _logger;

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
            await this.OnExecuteAsync(cancellationToken)
                      .ConfigureAwait(false);
        }
        finally
        {
            activity.Stop();
        }
    }
    protected abstract Task OnExecuteAsync(CancellationToken cancellationToken);
}
