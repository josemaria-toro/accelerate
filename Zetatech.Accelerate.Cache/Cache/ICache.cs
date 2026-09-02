using System;

namespace Zetatech.Accelerate.Cache;

public interface ICache : IDisposable
{
    Boolean Add<TValue>(String key, TValue value, TimeSpan delta);
    void Clear();
    Boolean Contains(String key);
    TValue Get<TValue>(String key);
    TValue GetOrAdd<TValue>(String key, Func<TValue> retrieve, TimeSpan delta);
    Boolean Remove(String key);
}
