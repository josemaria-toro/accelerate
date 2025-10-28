using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Extensions methods for the <see cref="ICacheService"/> interface.
/// </summary>
public static class ICacheServiceExtensions
{
    /// <summary>
    /// Adds a value to the cache with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key to associate with the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    public static async Task AddAsync<TValue>(this ICacheService cacheService, String key, TValue value)
    {
        await Task.Run(() => cacheService.AddAsync(key, value));
    }
    /// <summary>
    /// Adds a value to the cache with the specified key and expiration time.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key to associate with the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    /// <param name="expiredAt">
    /// The date and time when the cached value expires.
    /// </param>
    public static async Task AddAsync<TValue>(this ICacheService cacheService, String key, TValue value, DateTime expiredAt)
    {
        await Task.Run(() => cacheService.Add(key, value, expiredAt));
    }
    /// <summary>
    /// Removes all items from the cache.
    /// </summary>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    public static async Task ClearAsync(this ICacheService cacheService)
    {
        await Task.Run(() => cacheService.Clear());
    }
    /// <summary>
    /// Determines whether the cache contains an item with the specified key.
    /// </summary>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    /// <returns>
    /// true if the cache contains an item with the specified key; otherwise, false.
    /// </returns>
    public static async Task<Boolean> ContainsAsync(this ICacheService cacheService, String key)
    {
        return await Task.FromResult(cacheService.Contains(key));
    }
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    /// <returns>
    /// The value associated with the specified key, or the default value if not found.
    /// </returns>
    public static async Task<TValue> GetAsync<TValue>(this ICacheService cacheService, String key)
    {
        return await Task.FromResult(cacheService.Get<TValue>(key));
    }
    /// <summary>
    /// Removes the item with the specified key from the cache.
    /// </summary>
    /// <param name="cacheService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    public static async Task RemoveAsync(this ICacheService cacheService, String key)
    {
        await Task.Run(() => cacheService.Remove(key));
    }
}