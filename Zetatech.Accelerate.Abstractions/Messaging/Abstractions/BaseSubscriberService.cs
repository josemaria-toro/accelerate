using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Tracking;

namespace Zetatech.Accelerate.Messaging.Abstractions;

/// <summary>
/// Represents a base class for implementing custom subscribers for receiving messages from a publisher.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be received.
/// </typeparam>
/// <typeparam name="TOptions">
/// The type of the configuration options for the subscriber. Must inherit from <see cref="BaseSubscriberServiceOptions"/>.
/// </typeparam>
public abstract class BaseSubscriberService<TMessage, TOptions> : ISubscriberService<TMessage> where TMessage : BaseMessage
                                                                                               where TOptions : BaseSubscriberServiceOptions
{
    private Boolean _disposed;
    private TOptions _options;
    private readonly ITrackingService _trackingService;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages subscriber.
    /// </param>
    /// <param name="trackingService">
    /// Service for tracking application data.
    /// </param>
    protected BaseSubscriberService(IOptions<TOptions> options, ITrackingService trackingService = null)
    {
        _options = options?.Value;
        _trackingService = trackingService;
    }

    /// <summary>
    /// Gets the options for the messages subscriber.
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
    /// Notify a new message.
    /// </summary>
    /// <param name="message">
    /// Message information.
    /// </param>
    protected abstract void OnMessageReceived(TMessage message);
    /// <summary>
    /// Subscribes the current subscriber to receive published messages.
    /// </summary>
    public abstract void Subscribe();
    /// <summary>
    /// Unsubscribes the current subscriber from receiving published messages.
    /// </summary>
    public abstract void Unsubscribe();
}