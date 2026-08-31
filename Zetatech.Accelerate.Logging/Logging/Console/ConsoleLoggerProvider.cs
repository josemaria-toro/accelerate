using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Logging.Console;

public sealed class ConsoleLoggerProvider : ILoggerProvider
{
    private readonly Channel<ConsoleLoggerEntry> _channel;
    private Boolean _disposed;
    private ConcurrentDictionary<String, ConsoleLogger> _loggers;
    private readonly IOptions<ConsoleLoggerOptions> _options;

    public ConsoleLoggerProvider(IOptions<ConsoleLoggerOptions> options,
                                 Channel<ConsoleLoggerEntry> channel)
    {
        _channel = channel ?? throw new ArgumentException("The provided channel must be a valid instance", nameof(options));
        _loggers = new ConcurrentDictionary<String, ConsoleLogger>();
        _options = options ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
    }

    public ILogger CreateLogger(String category)
    {
        if (String.IsNullOrEmpty(category))
        {
            throw new ArgumentException("The provided category is invalid", nameof(category));
        }

        return _loggers.GetOrAdd(category, x => new ConsoleLogger(_options, x, _channel.Writer));
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _channel.Writer.Complete();
        _loggers = null;

        GC.SuppressFinalize(this);
    }
}
