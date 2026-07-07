using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Caching;

public interface ICache : IDisposable
{
    Boolean Add<TValue>(String key,
                        TValue value,
                        TimeSpan delta);
    Task<Boolean> AddAsync<TValue>(String key,
                                   TValue value,
                                   TimeSpan delta,
                                   CancellationToken cancellationToken = default);
    void Clear();
    Task ClearAsync(CancellationToken cancellationToken = default);
    Boolean Contains(String key);
    Task<Boolean> ContainsAsync(String key,
                                CancellationToken cancellationToken = default);
    TValue Get<TValue>(String key);
    Task<TValue> GetAsync<TValue>(String key,
                                  CancellationToken cancellationToken = default);
    TValue GetOrAdd<TValue>(String key,
                            Func<TValue> retrieve,
                            TimeSpan delta);
    Task<TValue> GetOrAddAsync<TValue>(String key,
                            Func<TValue> retrieve,
                            TimeSpan delta,
                            CancellationToken cancellationToken = default);
    Boolean Remove(String key);
    Task<Boolean> RemoveAsync(String key,
                              CancellationToken cancellationToken = default);
}