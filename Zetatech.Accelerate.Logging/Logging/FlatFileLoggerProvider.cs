using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.ChannelEntries;
using Zetatech.Accelerate.Logging.Loggers;

namespace Zetatech.Accelerate.Logging;

public sealed class FlatFileLoggerProvider : ILoggerProvider
{
    private readonly Channel<FlatFileChannelEntry> _channel;
    private Boolean _disposed;
    private ConcurrentDictionary<String, FlatFileLogger> _loggers;
    private readonly FlatFileLoggerOptions _options;

    public FlatFileLoggerProvider(IOptions<FlatFileLoggerOptions> options)
    {
        _channel = Channel.CreateBounded<FlatFileChannelEntry>(new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true
        });
        _loggers = new ConcurrentDictionary<String, FlatFileLogger>();
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
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
