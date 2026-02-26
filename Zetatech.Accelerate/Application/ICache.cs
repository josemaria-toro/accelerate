using System;

namespace Zetatech.Accelerate.Application;

public interface ICache : IDisposable
{
    Boolean Add<TValue>(String key, TValue value);
    Boolean Add<TValue>(String key, TValue value, DateTime expiredAt);
    void Clear();
    Boolean Contains(String key);
    TValue Get<TValue>(String key);
    Boolean Remove(String key);
}