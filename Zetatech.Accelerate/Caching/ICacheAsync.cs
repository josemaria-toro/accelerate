using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Caching;

public static class ICacheAsync
{
    public static async Task<Boolean> AddAsync<TValue>(this ICache cache,
                                                       String key,
                                                       TValue value,
                                                       TimeSpan delta,
                                                       CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => cache.Add(key, value, delta), cancellationToken);
    }
    public static async Task ClearAsync(this ICache cache,
                                        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => cache.Clear(), cancellationToken);
    }
    public static async Task<Boolean> ContainsAsync(this ICache cache,
                                                    String key,
                                                    CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => cache.Contains(key), cancellationToken);
    }
    public static async Task<TValue> GetAsync<TValue>(this ICache cache,
                                                      String key,
                                                      CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => cache.Get<TValue>(key), cancellationToken);
    }
    public static async Task<TValue> GetOrAddAsync<TValue>(this ICache cache,
                                                           String key,
                                                           Func<TValue> retrieve,
                                                           TimeSpan delta,
                                                           CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => cache.GetOrAdd(key, retrieve, delta), cancellationToken);
    }
    public static async Task<Boolean> RemoveAsync(this ICache cache,
                                                  String key,
                                                  CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => cache.Remove(key), cancellationToken);
    }
}