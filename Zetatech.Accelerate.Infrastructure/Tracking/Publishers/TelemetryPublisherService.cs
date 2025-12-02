using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Infrastructure.Messaging;

namespace Zetatech.Accelerate.Infrastructure.Tracking.Publishers;

internal sealed class TelemetryPublisherService : RabbitMqPublisherService<TrackingMessage, RabbitMqPublisherServiceOptions>
{
    public TelemetryPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}
