using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Messages;
using Zetatech.Accelerate.Messaging.Publishers;

namespace Zetatech.Accelerate.Logging.Publishers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based publisher service for errors.
/// </summary>
internal sealed class ErrorsPublisherService : RabbitMqPublisherService<ErrorMessage, RabbitMqPublisherServiceOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the publisher service.
    /// </param>
    public ErrorsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options) : base(options)
    {
    }
}