using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Infrastructure.Messaging;

namespace Zetatech.Accelerate.Infrastructure.Tracking.Publishers;

internal sealed class EventsPublisherService : RabbitMqPublisherService<TrackingMessage, RabbitMqPublisherServiceOptions>
{
    public EventsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}
