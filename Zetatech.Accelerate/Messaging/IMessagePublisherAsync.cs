using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Messaging;

public static class IMessagePublisherAsync
{
    public static async Task<Guid> PublishAsync<TBody>(this IMessagePublisher messagePublisher,
                                                       TBody body,
                                                       String queueName = null,
                                                       CancellationToken cancellationToken = default) where TBody : class
    {
        return await Task.Run(() => messagePublisher.Publish(body, queueName), cancellationToken);
    }
}
