using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Logging.Providers;

internal sealed class ConsoleLoggerProvider : ILoggerProvider
{
    private Boolean _disposed;
    private ConcurrentDictionary<String, ConsoleLogger> _loggers;
    private readonly IOptions<ConsoleLoggerOptions> _options;
    private SemaphoreSlim _semaphore;

    public ConsoleLoggerProvider(IOptions<ConsoleLoggerOptions> options)
    {
        _loggers = new ConcurrentDictionary<String, ConsoleLogger>();
        _options = options ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public ILogger CreateLogger(String category)
    {
        if (String.IsNullOrEmpty(category))
        {
            throw new ArgumentException("The provided category is invalid", nameof(category));
        }

        return _loggers.GetOrAdd(category, x => new ConsoleLogger(_options, x, _semaphore));
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _semaphore.Wait();
        _loggers = null;
        _semaphore = null;

        GC.SuppressFinalize(this);
    }
}
