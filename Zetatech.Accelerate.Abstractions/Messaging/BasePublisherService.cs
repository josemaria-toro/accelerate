using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Represents the base class for implementing custom publishers for broadcasting messages to subscribers.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be published.
/// </typeparam>
/// <typeparam name="TOptions">
/// The type of the configuration options for the publisher. Must inherit from <see cref="BasePublisherServiceOptions"/>.
/// </typeparam>
public abstract class BasePublisherService<TMessage, TOptions> : BaseDisposable, IPublisherService<TMessage> where TMessage : BaseMessage
                                                                                                             where TOptions : BasePublisherServiceOptions
{
    private Boolean _disposed;
    private ILogger _logger;
    private TOptions _options;
    private List<ISubscriberService<TMessage>> _subscriberServices;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages publisher.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    protected BasePublisherService(IOptions<TOptions> options, ILoggerFactory loggerFactory = null)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
        _options = options?.Value;
        _subscriberServices = [];
    }

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets the options for the messages publisher.
    /// </summary>
    protected TOptions Options => _options;

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
            _subscriberServices = null;
        }
    }
    /// <summary>
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    public virtual void Publish(TMessage message)
    {
        _logger?.LogDebug("Publishing message to subscribers");

        if (message == null)
        {
            throw new ArgumentException("The message to publish cannot be null", nameof(message));
        }

        foreach (var subscriberService in _subscriberServices)
        {
            Task.Run(() => subscriberService.Receive(message));
        }
    }
    /// <summary>
    /// Subscribes the specified subscriber to receive published messages.
    /// </summary>
    /// <param name="subscriberService">
    /// The subscriber service to add.
    /// </param>
    public virtual void Subscribe(ISubscriberService<TMessage> subscriberService)
    {
        _logger?.LogDebug("Adding a subscriber");

        if (subscriberService == null)
        {
            throw new ArgumentException("The subscriber service to subscribe cannot be null", nameof(subscriberService));
        }

        _subscriberServices.Add(subscriberService);
    }
    /// <summary>
    /// Unsubscribes the specified subscriber from receiving published messages.
    /// </summary>
    /// <param name="subscriberService">
    /// The subscriber service to remove.
    /// </param>
    public virtual void Unsubscribe(ISubscriberService<TMessage> subscriberService)
    {
        _logger?.LogDebug("Removing a subscriber");

        if (subscriberService == null)
        {
            throw new ArgumentException("The subscriber service to unsubscribe cannot be null", nameof(subscriberService));
        }

        _subscriberServices.Remove(subscriberService);
    }
}