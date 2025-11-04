using System;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Provides the interface for implementing custom subscriber services for receiving messages from a publishers.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be received.
/// </typeparam>
public interface ISubscriberService<TMessage> : IDisposable where TMessage : IMessage
{
    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    void Receive(TMessage message);
}