using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Extensions methods for the <see cref="ISubscriberService{TMessage}"/> interface.
/// </summary>
public static class ISubscriberServiceExtensions
{
    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="subscriberService">
    /// The instance of the subscriber service.
    /// </param>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public static async Task ReceiveAsync<TMessage>(this ISubscriberService<TMessage> subscriberService, TMessage message) where TMessage : IMessage
    {
        await Task.Run(() => subscriberService.Receive(message));
    }
}