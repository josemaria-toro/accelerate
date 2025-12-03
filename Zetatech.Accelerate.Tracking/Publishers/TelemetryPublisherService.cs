using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Messaging.Services;
using Zetatech.Accelerate.Tracking.Services;

namespace Zetatech.Accelerate.Tracking.Publishers;

internal sealed class TelemetryPublisherService : RabbitMqPublisherService<TrackingMessage, RabbitMqPublisherServiceOptions>
{
    public TelemetryPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}
