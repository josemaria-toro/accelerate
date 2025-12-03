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
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    void Publish(TMessage message);
}
