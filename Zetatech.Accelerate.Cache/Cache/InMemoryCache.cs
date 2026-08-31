using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Cache;

public sealed class InMemoryCache : ICache
{
    private ConcurrentDictionary<String, InMemoryCacheItem> _dictionary;
    private Boolean _disposed;
    private readonly InMemoryCacheOptions _options;

    public InMemoryCache(IOptions<InMemoryCacheOptions> options)
    {
        _dictionary = new();
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
    }

    internal ConcurrentDictionary<String, InMemoryCacheItem> Dictionary => _dictionary;

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
        _dictionary.Clear();
    }
    public Boolean Contains(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

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

        GC.SuppressFinalize(this);
    }
    public TValue Get<TValue>(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        var value = default(TValue);

        if (_dictionary.TryGetValue(key, out var cacheObject))
        {
            if (!cacheObject.IsExpired)
            {
                value = (TValue)cacheObject.Value;
            }
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

        return _dictionary.TryRemove(key, out var _);
    }
}
