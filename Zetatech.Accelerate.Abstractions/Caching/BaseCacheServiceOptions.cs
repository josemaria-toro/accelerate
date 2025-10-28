using System;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Represents the base options for configuring a cache service.
/// </summary>
public abstract class BaseCacheServiceOptions : BaseCloneable
{
    /// <summary>
    /// Gets or sets the default expiration time (in minutes) for cache entries.
    /// </summary>
    public Int32 DefaultExpirationTime { get; set; }
    /// <summary>
    /// Gets or sets the maximum number of entries allowed in the cache.
    /// </summary>
    public Int32 MaxSize { get; set; }
}