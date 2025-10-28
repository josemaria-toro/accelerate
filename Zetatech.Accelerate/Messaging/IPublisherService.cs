using Microsoft.Extensions.Logging;
using System;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Provides the interface for implementing custom publisher services for broadcasting messages to subscribers.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be published.
/// </typeparam>
public interface IPublisherService<TMessage> : IDisposable where TMessage : IMessage
{
    /// <summary>
    /// Gets or sets the factory to create instances of loggers.
    /// </summary>
    ILoggerFactory LoggerFactory { get; set; }
    
    /// <summary>
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    void Publish(TMessage message);
    /// <summary>
    /// Subscribes the specified subscriber to receive published messages.
    /// </summary>
    /// <param name="subscriberService">
    /// The subscriber service to add.
    /// </param>
    void Subscribe(ISubscriberService<TMessage> subscriberService);
    /// <summary>
    /// Unsubscribes the specified subscriber from receiving published messages.
    /// </summary>
    /// <param name="subscriberService">
    /// The subscriber service to remove.
    /// </param>
    void Unsubscribe(ISubscriberService<TMessage> subscriberService);
}
