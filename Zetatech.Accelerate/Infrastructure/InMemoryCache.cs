using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Application;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class InMemoryCache : ICache
{
    private ConcurrentDictionary<String, InMemoryCacheObject> _dictionary;
    private Boolean _disposed;
    private InMemoryCacheOptions _options;

    public InMemoryCache(IOptions<InMemoryCacheOptions> options)
    {
        _dictionary = new();
        _options = options.Value;
    }

    public Boolean Add<TValue>(String key, TValue value)
    {
        return Add(key, value, DateTime.UtcNow.AddMinutes(_options.DefaultExpirationTime));
    }
    public Boolean Add<TValue>(String key, TValue value, DateTime expiredAt)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentException("The provided value is invalid", nameof(value));
        }

        if (expiredAt.ToUniversalTime() <= DateTime.UtcNow)
        {
            throw new ArgumentException("The expiration date is invalid", nameof(key));
        }

        Purge();

        if (_dictionary.Count >= _options.MaxSize)
        {
            throw new OverflowException("The number of elements in the cache is up to the maximum size");
        }

        return _dictionary.TryAdd(key, new InMemoryCacheObject
        {
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = expiredAt.ToUniversalTime(),
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

        Purge();

        return _dictionary.ContainsKey(key);
    }
    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _dictionary = null;
        _options = null;

        GC.SuppressFinalize(this);
    }
    public TValue Get<TValue>(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        Purge();

        var value = default(TValue);

        if (_dictionary.TryGetValue(key, out var inMemoryCacheObject))
        {
            value = (TValue)inMemoryCacheObject.Value;
        }

        return value;
    }
    private void Purge()
    {
        var expiredObjects = _dictionary.Values.Where(x => x.ExpiredAt.ToUniversalTime() < DateTime.UtcNow);

        foreach (var expiredObject in expiredObjects)
        {
            _dictionary.TryRemove(expiredObject.Key, out var _);
        }
    }
    public Boolean Remove(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key is invalid", nameof(key));
        }

        Purge();

        return _dictionary.TryRemove(key, out var _);
    }
}