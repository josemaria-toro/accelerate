using System;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class InMemoryCacheOptions
{
    public Int32 DefaultExpirationTime { get; set; }
    public Int32 MaxSize { get; set; }
}