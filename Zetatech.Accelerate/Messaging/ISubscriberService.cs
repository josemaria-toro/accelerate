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
    /// Subscribes the current subscriber to receive published messages.
    /// </summary>
    void Subscribe();
    /// <summary>
    /// Unsubscribes the current subscriber from receiving published messages.
    /// </summary>
    void Unsubscribe();
}