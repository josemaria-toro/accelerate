using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Caching;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Infrastructure.Abstractions;

/// <summary>
/// Represents the base class for implementing custom cache services.
/// </summary>
public abstract class BaseCachingService<TOptions> : ICachingService where TOptions : BaseCachingServiceOptions
{
    private Boolean _disposed;
    private TOptions _options;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the cache service.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseCachingService(IOptions<TOptions> options, ITrackingService trackingService = null)
    {
        _options = options.Value;
        _trackingService = trackingService;
    }

    /// <summary>
    /// Gets the options for the cache service.
    /// </summary>
    protected TOptions Options => _options;
    /// <summary>
    /// Gets the service for tracking application data.
    /// </summary>
    protected ITrackingService TrackingService => _trackingService;

    /// <summary>
    /// Adds a value to the cache with the specified key.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The unique key for the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    public abstract void Add<TValue>(String key, TValue value);
    /// <summary>
    /// Adds a value to the cache with the specified key and expiration date.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to cache.
    /// </typeparam>
    /// <param name="key">
    /// The unique key for the cached value.
    /// </param>
    /// <param name="value">
    /// The value to cache.
    /// </param>
    /// <param name="expiredAt">
    /// The date and time when the cached value expires.
    /// </param>
    public abstract void Add<TValue>(String key, TValue value, DateTime expiredAt);
    /// <summary>
    /// Removes all entries from the cache.
    /// </summary>
    public abstract void Clear();
    /// <summary>
    /// Determines whether the cache contains a value with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    public abstract Boolean Contains(String key);
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        if (disposing)
        {
            _options = null;
        }
    }
    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="key">
    /// The key of the cached value to retrieve.
    /// </param>
    public abstract TValue Get<TValue>(String key);
    /// <summary>
    /// Removes the value with the specified key from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the cached value to remove.
    /// </param>
    public abstract void Remove(String key);
}