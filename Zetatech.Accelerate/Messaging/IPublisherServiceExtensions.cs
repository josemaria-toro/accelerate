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
    /// <summary>
    /// Subscribes the specified subscriber to receive published messages.
    /// </summary>
    /// <param name="publisherService">
    /// The instance of the publisher service.
    /// </param>
    /// <param name="subscriberService">
    /// The subscriber to add.
    /// </param>
    public static async Task SubscribeAsync<TMessage>(this IPublisherService<TMessage> publisherService, ISubscriberService<TMessage> subscriberService) where TMessage : IMessage
    {
        await Task.Run(() => publisherService.SubscribeAsync(subscriberService));
    }
    /// <summary>
    /// Unsubscribes the specified subscriber from receiving published messages.
    /// </summary>
    /// <param name="publisherService">
    /// The instance of the publisher service.
    /// </param>
    /// <param name="subscriberService">
    /// The subscriber to remove.
    /// </param>
    public static async Task UnsubscribeAsync<TMessage>(this IPublisherService<TMessage> publisherService, ISubscriberService<TMessage> subscriberService) where TMessage : IMessage
    {
        await Task.Run(() => publisherService.UnsubscribeAsync(subscriberService));
    }
}
