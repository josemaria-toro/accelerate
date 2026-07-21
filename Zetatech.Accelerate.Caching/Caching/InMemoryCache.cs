using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Caching;

internal sealed class InMemoryCache : ICache
{
    private ConcurrentDictionary<String, InMemoryCacheItem> _dictionary;
    private Boolean _disposed;
    private readonly ILogger _logger;
    private readonly InMemoryCacheOptions _options;
    private PeriodicTimer _timer;

    public InMemoryCache(IOptions<InMemoryCacheOptions> options,
                         ILoggerFactory loggerFactory)
    {
        _dictionary = new();
        _logger = loggerFactory.CreateLogger(GetType().Name);
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
        _ = ClearExpiredObjectsAsync();
    }

    public Boolean Add<TValue>(String key,
                               TValue value,
                               TimeSpan delta)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentException("The provided value must be a valid instance", nameof(value));
        }

        if (_dictionary.Count >= _options.MaxItems)
        {
            throw new OverflowException("The number of items in the cache is upper than the maximum allowed");
        }

        _logger.LogDebug($"Adding item with key '{key}'");

        return _dictionary.TryAdd(key, new InMemoryCacheItem
        {
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddSeconds(delta.TotalSeconds),
            Key = key,
            Value = value
        });
    }
    public void Clear()
    {
        _logger.LogDebug("Removing all existing items");
        _dictionary.Clear();
    }
    private async Task ClearExpiredObjectsAsync()
    {
        try
        {
            while (await _timer.WaitForNextTickAsync())
            {
                _logger.LogDebug("Removing expired items");

                var expiredObjects = _dictionary.Values.Where(x => x.IsExpired);

                foreach (var expiredObject in expiredObjects)
                {
                    _ = _dictionary.TryRemove(expiredObject.Key, out var _);
                    _logger.LogDebug($"Item with key '{expiredObject.Key}' was removed");
                }
            }
        }
        catch
        {
            _timer.Dispose();
        }
    }
    public Boolean Contains(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        _logger.LogDebug($"Checking if item with key '{key}' exists");

        return _dictionary.ContainsKey(key) && !_dictionary[key].IsExpired;
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _dictionary = null;
        _timer = null;

        GC.SuppressFinalize(this);
    }
    public TValue Get<TValue>(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        _logger.LogDebug($"Getting item with key '{key}'");

        var value = default(TValue);

        if (_dictionary.TryGetValue(key, out var cacheObject))
        {
            if (cacheObject.IsExpired)
            {
                _logger.LogDebug($"The item with key '{key}' is expired");
            }
            else
            {
                value = (TValue)cacheObject.Value;
            }
        }
        else
        {
            _logger.LogDebug($"Item with key '{key}' cannot be found");
        }

        return value;
    }
    public TValue GetOrAdd<TValue>(String key, Func<TValue> retrieve, TimeSpan delta)
    {
        var value = Get<TValue>(key);

        if (value == null)
        {
            value = retrieve();

            if (value != null)
            {
                Add(key, value, delta);
            }
        }

        return value;
    }
    public Boolean Remove(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        _logger.LogDebug($"Removing item with key '{key}'");

        if (_dictionary.TryRemove(key, out var _))
        {
            _logger.LogDebug($"Item with key '{key}' was removed");
            return true;
        }
        else
        {
            _logger.LogDebug($"Item with key '{key}' cannot be found");
            return false;
        }
    }
}