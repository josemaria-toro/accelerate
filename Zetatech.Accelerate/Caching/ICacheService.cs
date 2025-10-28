using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Provides the interface for implementing custom cache services.
/// </summary>
public interface ICacheService : IDisposable
{
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    ILoggerFactory LoggerFactory { get; set; }

    /// <summary>
    /// Adds a value to the cache with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The key to associate with the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    void Add<TValue>(String key, TValue value);
    /// <summary>
    /// Adds a value to the cache with the specified key and expiration time.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The key to associate with the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    /// <param name="expiredAt">
    /// The date and time when the cached value expires.
    /// </param>
    void Add<TValue>(String key, TValue value, DateTime expiredAt);
    /// <summary>
    /// Removes all items from the cache.
    /// </summary>
    void Clear();
    /// <summary>
    /// Determines whether the cache contains an item with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    /// <returns>
    /// true if the cache contains an item with the specified key; otherwise, false.
    /// </returns>
    Boolean Contains(String key);
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    /// <returns>
    /// The value associated with the specified key, or the default value if not found.
    /// </returns>
    TValue Get<TValue>(String key);
    /// <summary>
    /// Removes the item with the specified key from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    void Remove(String key);
}