using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Zetatech.Accelerate.Caching;

/// <summary>
/// Represents the base class for implementing custom cache services.
/// </summary>
public abstract class BaseCacheService<TOptions> : BaseDisposable, ICacheService where TOptions : BaseCacheServiceOptions
{
    private Boolean _disposed;
    private ILogger _logger;
    private ILoggerFactory _loggerFactory;
    private TOptions _options;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the cache service.
    /// </param>
    protected BaseCacheService(IOptions<TOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    public ILoggerFactory LoggerFactory
    {
        get => _loggerFactory;
        set
        {
            _loggerFactory = value;
            _logger = _loggerFactory?.CreateLogger(GetType().Name);
        }
    }
    /// <summary>
    /// Gets the options for the messages publisher.
    /// </summary>
    protected TOptions Options => _options;

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
    /// <returns>
    /// <c>true</c> if the cache contains an entry with the specified key; otherwise, <c>false</c>.
    /// </returns>
    public abstract Boolean Contains(String key);
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose(disposing);

        if (disposing)
        {
            _logger = null;
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
    /// <returns>
    /// The value associated with the specified key, or the default value if not found.
    /// </returns>
    public abstract TValue Get<TValue>(String key);
    /// <summary>
    /// Removes the value with the specified key from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the cached value to remove.
    /// </param>
    public abstract void Remove(String key);
}