using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Extensions methods for the <see cref="ISubscriberService{TMessage}"/> interface.
/// </summary>
public static class ISubscriberServiceExtensions
{
    /// <summary>
    /// Subscribes the current subscriber to receive published messages.
    /// </summary>
    public static async void SubscribeAsync<TMessage>(this ISubscriberService<TMessage> subscriberService) where TMessage : IMessage
    {
        await Task.Run(() => subscriberService.Subscribe());
    }
    /// <summary>
    /// Unsubscribes the current subscriber from receiving published messages.
    /// </summary>
    public static async void UnsubscribeAsync<TMessage>(this ISubscriberService<TMessage> subscriberService) where TMessage : IMessage
    {
        await Task.Run(() => subscriberService.Unsubscribe());
    }
}