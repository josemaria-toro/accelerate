using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

public static class IMessageSubscriberAsync
{
    public static async Task SubscribeAsync<TBody>(this IMessageSubscriber<TBody> messageSubscriber,
                                                   CancellationToken cancellationToken = default) where TBody : class
    {
        await Task.Run(() => messageSubscriber.Subscribe(), cancellationToken);
    }
    public static async Task UnsubscribeAsync<TBody>(this IMessageSubscriber<TBody> messageSubscriber,
                                                     CancellationToken cancellationToken = default) where TBody : class
    {
        await Task.Run(() => messageSubscriber.Unsubscribe(), cancellationToken);
    }
}