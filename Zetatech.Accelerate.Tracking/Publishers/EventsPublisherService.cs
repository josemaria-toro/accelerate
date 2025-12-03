using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Messaging.Services;
using Zetatech.Accelerate.Tracking.Services;

namespace Zetatech.Accelerate.Tracking.Publishers;

internal sealed class EventsPublisherService : RabbitMqPublisherService<TrackingMessage, RabbitMqPublisherServiceOptions>
{
    public EventsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}
