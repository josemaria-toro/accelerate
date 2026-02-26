using System;

namespace Zetatech.Accelerate.Infrastructure;

internal sealed class InMemoryCacheObject
{
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public String Key { get; set; }
    public Object Value { get; set; }
}