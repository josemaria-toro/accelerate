using System;

namespace Zetatech.Accelerate.Caching.Services;

/// <summary>
/// Represents a cache object stored in the cache.
/// </summary>
internal sealed class MemoryCachingServiceObject
{
    /// <summary>
    /// Gets or sets the date and time when the object was added to the cache.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the object is considered expired and invalid for use.
    /// </summary>
    public DateTime ExpiredAt { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier used to store and retrieve the cached object.
    /// </summary>
    public String Key { get; set; }
    /// <summary>
    /// Gets or sets the actual data stored in the cache.
    /// </summary>
    public Object Value { get; set; }
}