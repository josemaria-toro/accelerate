using System;

namespace Zetatech.Accelerate.Caching;

internal sealed class InMemoryCacheItem
{
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public Boolean IsExpired => ExpiredAt < DateTime.UtcNow;
    public String Key { get; set; }
    public Object Value { get; set; }
}