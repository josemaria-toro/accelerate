using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Cache;

/// <summary>
/// Base class for services to manage cache objects.
/// </summary>
/// <typeparam name="TOptions">
/// Type of configuration options.
/// </typeparam>
public abstract class CacheService<TOptions> : ICacheService where TOptions : CacheServiceOptions
{
    private Boolean _disposed;
    private TOptions _options;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for memory cache service.
    /// </param>
    protected CacheService(IOptions<TOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("Options for cache service cannot be null", nameof(options));
    }

    /// <summary>
    /// Configuration options.
    /// </summary>
    protected TOptions Options => _options;

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
    public abstract void Add<TValue>(String key, TValue value);
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
    public abstract void Add<TValue>(String key, TValue value, DateTime expiredAt);
    /// <summary>
    /// Remove all values in the cache.
    /// </summary>
    public abstract void Clear();
    /// <summary>
    /// Check if a key is in the cache.
    /// </summary>
    /// <param name="key">
    /// Key to check if it's contained.
    /// </param>
    public abstract Boolean Contains(String key);
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        if (disposing)
        {
            _options = null;
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
    public abstract TValue Get<TValue>(String key);
    /// <summary>
    /// Remove the value with the specified key.
    /// </summary>
    /// <param name="key">
    /// Unique key to identify the value in the cache.
    /// </param>
    public abstract void Remove(String key);
}