using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Provides the interface for implementing custom cache services.
/// </summary>
public interface ICachingService : IDisposable
{
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
    Task AddAsync<TValue>(String key, TValue value);
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
    Task AddAsync<TValue>(String key, TValue value, DateTime expiredAt);
    /// <summary>
    /// Removes all items from the cache.
    /// </summary>
    Task ClearAsync();
    /// <summary>
    /// Determines whether the cache contains an item with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    Task<Boolean> ContainsAsync(String key);
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    Task<TValue> GetAsync<TValue>(String key);
    /// <summary>
    /// Removes the item with the specified key from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    Task RemoveAsync(String key);
}