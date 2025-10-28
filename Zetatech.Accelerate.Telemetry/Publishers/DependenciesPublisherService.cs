using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Telemetry.Messages;
using Zetatech.Accelerate.Messaging.Publishers;

namespace Zetatech.Accelerate.Telemetry.Publishers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based publisher service for dependencies.
/// </summary>
internal sealed class DependenciesPublisherService : RabbitMqPublisherService<DependencyMessage, RabbitMqPublisherServiceOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the publisher service.
    /// </param>
    public DependenciesPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}