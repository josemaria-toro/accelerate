using System;

namespace Zetatech.Accelerate.Caching.Abstractions;

/// <summary>
/// Represents the base options for configuring a cache service.
/// </summary>
public abstract class BaseCachingServiceOptions
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