using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Logging.FlatFile;

public sealed class FlatFileLoggerProvider : ILoggerProvider
{
    private readonly Channel<FlatFileLoggerEntry> _channel;
    private Boolean _disposed;
    private ConcurrentDictionary<String, FlatFileLogger> _loggers;
    private readonly IOptions<FlatFileLoggerOptions> _options;

    public FlatFileLoggerProvider(IOptions<FlatFileLoggerOptions> options,
                                  Channel<FlatFileLoggerEntry> channel)
    {
        _channel = channel ?? throw new ArgumentException("The provided channel must be a valid instance", nameof(options));
        _loggers = new ConcurrentDictionary<String, FlatFileLogger>();
        _options = options ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
    }

    public ILogger CreateLogger(String category)
    {
        if (String.IsNullOrEmpty(category))
        {
            throw new ArgumentException("The provided category is invalid", nameof(category));
        }

        return _loggers.GetOrAdd(category, x => new FlatFileLogger(_options, x, _channel.Writer));
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
