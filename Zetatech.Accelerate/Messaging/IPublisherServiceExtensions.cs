using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Extensions methods for the <see cref="IPublisherService{TMessage}"/> interface.
/// </summary>
public static class IPublisherServiceExtensions
{
    /// <summary>
    /// Publishes a message to all subscribed instances.
    /// </summary>
    /// <param name="publisherService">
    /// The instance of the publisher service.
    /// </param>
    /// <param name="message">
    /// The message to publish.
    /// </param>
    public static async Task PublishAsync<TMessage>(this IPublisherService<TMessage> publisherService, TMessage message) where TMessage : IMessage
    {
        await Task.Run(() => publisherService.Publish(message));
    }
}
