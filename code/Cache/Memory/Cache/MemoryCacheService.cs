using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Accelerate.Cache;

/// <summary>
/// Infrastructure service to manage cache objects based on physical memory.
/// </summary>
public sealed class MemoryCacheService : CacheService<MemoryCacheServiceOptions>
{
    private ConcurrentDictionary<String, MemoryCacheServiceObject> _dictionary;
    private Boolean _disposed;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for memory cache service.
    /// </param>
    public MemoryCacheService(IOptions<MemoryCacheServiceOptions> options) : base(options)
    {
        _dictionary = new ConcurrentDictionary<String, MemoryCacheServiceObject>();
    }

    /// <summary>
    /// Add a value to cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to add.
    /// </typeparam>
    /// <param name="key">
    /// Unique key to identify the value in the cache.
    /// </param>
    /// <param name="value">
    /// Value to add to cache.
    /// </param>
    public override void Add<TValue>(String key, TValue value)
    {
        Add(key, value, DateTime.UtcNow.AddMinutes(Options.DefaultExpirationTime));
    }
    /// <summary>
    /// Add a value to cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to add.
    /// </typeparam>
    /// <param name="key">
    /// Unique key to identify the value in the cache.
    /// </param>
    /// <param name="value">
    /// Value to add to cache.
    /// </param>
    /// <param name="expiredAt">
    /// Cache value expiration date.
    /// </param>
    public override void Add<TValue>(String key, TValue value, DateTime expiredAt)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Value cannot be added to the cache service because the provided key is invalid", nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentException("Value cannot be added to the cache service because the provided value is invalid", nameof(value));
        }

        Purge();

        if (_dictionary.Count >= Options.MaxCacheSize)
        {
            throw new OverflowException("Value cannot be added because the maximum size is surpassed");
        }

        if (_dictionary.ContainsKey(key))
        {
            throw new ConflictException("Value cannot be added because already exists an Object with the same key");
        }

        if (DateTime.UtcNow < expiredAt.ToUniversalTime())
        {
            _dictionary.TryAdd(key, new MemoryCacheServiceObject
            {
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = expiredAt.ToUniversalTime(),
                Key = key,
                Value = value
            });
        }
    }
    /// <summary>
    /// Remove all values in the cache.
    /// </summary>
    public override void Clear()
    {
        _dictionary.Clear();
    }
    /// <summary>
    /// Check if a key is in the cache.
    /// </summary>
    /// <param name="key">
    /// Key to check if it's contained.
    /// </param>
    public override Boolean Contains(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be checked because the provided key is invalid", nameof(key));
        }

        Purge();

        return _dictionary.ContainsKey(key);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        base.Dispose(disposing);

        if (disposing)
        {
            _dictionary = null;
        }

        _disposed = true;
    }
    /// <summary>
    /// Get the value with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to get.
    /// </typeparam>
    /// <param name="key">
    /// Unique key to identify the value in the cache.
    /// </param>
    public override TValue Get<TValue>(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Value cannot be retrieved from the cache service because the provided key is invalid", nameof(key));
        }

        Purge();

        if (!_dictionary.TryGetValue(key, out var memoryCacheServiceObject))
        {
            throw new NotFoundException("Value cannot be retrieved from the cache service because the key was not found");
        }

        return (TValue)memoryCacheServiceObject.Value;
    }
    /// <summary>
    /// Remove all expired values in the cache.
    /// </summary>
    private void Purge()
    {
        var expiredObjects = _dictionary.Values.Where(x => x.ExpiredAt < DateTime.UtcNow);

        foreach (var expiredObject in expiredObjects)
        {
            _dictionary.TryRemove(expiredObject.Key, out var _);
        }
    }
    /// <summary>
    /// Remove the value with the specified key.
    /// </summary>
    /// <param name="key">
    /// Unique key to identify the value in the cache.
    /// </param>
    public override void Remove(String key)
    {
        if (String.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Value cannot be removed from the cache service because the provided key is invalid", nameof(key));
        }

        Purge();

        if (!_dictionary.ContainsKey(key))
        {
            throw new NotFoundException("Value cannot be removed from the cache service because the key was not found");
        }

        _dictionary.TryRemove(key, out var _);
    }
}