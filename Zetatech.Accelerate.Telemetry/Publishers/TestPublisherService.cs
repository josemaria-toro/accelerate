using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Telemetry.Messages;
using Zetatech.Accelerate.Messaging.Publishers;

namespace Zetatech.Accelerate.Telemetry.Publishers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based publisher service for tests.
/// </summary>
internal sealed class TestsPublisherService : RabbitMqPublisherService<TestMessage, RabbitMqPublisherServiceOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the publisher service.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    public TestsPublisherService(IOptions<RabbitMqPublisherServiceOptions> options, ILoggerFactory loggerFactory = null) : base(options, loggerFactory)
    {
    }
}