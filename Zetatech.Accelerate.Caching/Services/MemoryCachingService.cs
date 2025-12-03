using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Linq;
using Zetatech.Accelerate.Caching.Abstractions;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Caching.Services;

/// <summary>
/// Represents an implementation for a custom memory-based cache service implementation.
/// </summary>
public sealed class MemoryCachingService : BaseCachingService<MemoryCachingServiceOptions>
{
    private ConcurrentDictionary<String, MemoryCachingServiceObject> _dictionary;
    private Boolean _disposed;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the memory cache service.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    public MemoryCachingService(IOptions<MemoryCachingServiceOptions> options, ITrackingService trackingService = null) : base(options, trackingService)
    {
        _dictionary = new();
    }

    /// <summary>
    /// Adds a value to the cache with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The unique key for the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    public override void Add<TValue>(String key, TValue value)
    {
        Add(key, value, DateTime.UtcNow.AddMinutes(Options.DefaultExpirationTime));
    }
    /// <summary>
    /// Adds a value to the cache with the specified key and expiration date.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The unique key for the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    /// <param name="expiredAt">
    /// The date and time when the cached value expires.
    /// </param>
    public override void Add<TValue>(String key, TValue value, DateTime expiredAt)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key cannot be null", nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentException("The value to add to the cache cannot be null", nameof(key));
        }

        if (expiredAt.ToUniversalTime() <= DateTime.UtcNow)
        {
            throw new ArgumentException("The expiration date cannot be earlier than now", nameof(key));
        }

        Purge();

        if (_dictionary.Count >= Options.MaxSize)
        {
            throw new OverflowException("Value cannot be added because the cache is full");
        }

        _dictionary.TryAdd(key, new MemoryCachingServiceObject
        {
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = expiredAt.ToUniversalTime(),
            Key = key,
            Value = value
        });
    }
    /// <summary>
    /// Removes all entries from the cache.
    /// </summary>
    public override void Clear()
    {
        _dictionary.Clear();
    }
    /// <summary>
    /// Determines whether the cache contains a value with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    public override Boolean Contains(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key cannot be null", nameof(key));
        }

        Purge();

        return _dictionary.ContainsKey(key);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
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
            _dictionary = null;
        }
    }
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    public override TValue Get<TValue>(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key cannot be null", nameof(key));
        }

        Purge();

        var value = default(TValue);

        if (_dictionary.TryGetValue(key, out var memoryCacheServiceObject))
        {
            value = (TValue)memoryCacheServiceObject.Value;
        }

        return value;
    }
    /// <summary>
    /// Removes expired cache entries from the dictionary.
    /// </summary>
    private void Purge()
    {
        var expiredObjects = _dictionary.Values.Where(x => x.ExpiredAt.ToUniversalTime() < DateTime.UtcNow);

        foreach (var expiredObject in expiredObjects)
        {
            _dictionary.TryRemove(expiredObject.Key, out var _);
        }
    }
    /// <summary>
    /// Removes the value with the specified key from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the cached value to remove.
    /// </param>
    public override void Remove(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The provided key cannot be null", nameof(key));
        }

        Purge();

        _dictionary.TryRemove(key, out var _);
    }
}