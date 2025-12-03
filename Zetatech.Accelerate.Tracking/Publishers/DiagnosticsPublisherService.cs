using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Messaging.Services;
using Zetatech.Accelerate.Tracking.Services;

namespace Zetatech.Accelerate.Tracking.Publishers;

internal sealed class DiagnosticsPublisherService : RabbitMqPublisherService<TrackingMessage, RabbitMqPublisherServiceOptions>
{
    public DiagnosticsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}
