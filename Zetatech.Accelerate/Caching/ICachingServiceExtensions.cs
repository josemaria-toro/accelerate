using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Extensions methods for the <see cref="ICachingService"/> interface.
/// </summary>
public static class ICachingServiceExtensions
{
    /// <summary>
    /// Adds a value to the cache with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="cachingService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key to associate with the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    public static async Task AddAsync<TValue>(this ICachingService cachingService, String key, TValue value)
    {
        await Task.Run(() => cachingService.AddAsync(key, value));
    }
    /// <summary>
    /// Adds a value to the cache with the specified key and expiration time.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="cachingService">
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
    public static async Task AddAsync<TValue>(this ICachingService cachingService, String key, TValue value, DateTime expiredAt)
    {
        await Task.Run(() => cachingService.Add(key, value, expiredAt));
    }
    /// <summary>
    /// Removes all items from the cache.
    /// </summary>
    /// <param name="cachingService">
    /// The instance of the caching service.
    /// </param>
    public static async Task ClearAsync(this ICachingService cachingService)
    {
        await Task.Run(() => cachingService.Clear());
    }
    /// <summary>
    /// Determines whether the cache contains an item with the specified key.
    /// </summary>
    /// <param name="cachingService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    public static async Task<Boolean> ContainsAsync(this ICachingService cachingService, String key)
    {
        return await Task.FromResult(cachingService.Contains(key));
    }
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="cachingService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    public static async Task<TValue> GetAsync<TValue>(this ICachingService cachingService, String key)
    {
        return await Task.FromResult(cachingService.Get<TValue>(key));
    }
    /// <summary>
    /// Removes the item with the specified key from the cache.
    /// </summary>
    /// <param name="cachingService">
    /// The instance of the caching service.
    /// </param>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    public static async Task RemoveAsync(this ICachingService cachingService, String key)
    {
        await Task.Run(() => cachingService.Remove(key));
    }
}