using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Messaging.Abstractions;

/// <summary>
/// Represents the base class for implementing custom publishers for broadcasting messages to subscribers.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be published.
/// </typeparam>
/// <typeparam name="TOptions">
/// The type of the configuration options for the publisher. Must inherit from <see cref="BasePublisherServiceOptions"/>.
/// </typeparam>
public abstract class BasePublisherService<TMessage, TOptions> : IPublisherService<TMessage> where TMessage : BaseMessage
                                                                                             where TOptions : BasePublisherServiceOptions
{
    private Boolean _disposed;
    private TOptions _options;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages publisher.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BasePublisherService(IOptions<TOptions> options, ITrackingService trackingService = null)
    {
        _options = options?.Value;
        _trackingService = trackingService;
    }

    /// <summary>
    /// Gets the options for the messages publisher.
    /// </summary>
    protected TOptions Options => _options;
    /// <summary>
    /// Gets the service for tracking application data.
    /// </summary>
    protected ITrackingService TrackingService => _trackingService;

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
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    public abstract void Publish(TMessage message);
}