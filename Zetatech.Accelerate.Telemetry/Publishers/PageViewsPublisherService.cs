using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Telemetry.Messages;
using Zetatech.Accelerate.Messaging.Publishers;

namespace Zetatech.Accelerate.Telemetry.Publishers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based publisher service for page views.
/// </summary>
internal sealed class PageViewsPublisherService : RabbitMqPublisherService<PageViewMessage, RabbitMqPublisherServiceOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the publisher service.
    /// </param>
    public PageViewsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}