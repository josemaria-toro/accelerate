using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Telemetry.Entities;
using Zetatech.Accelerate.Telemetry.Messages;

namespace Zetatech.Accelerate.Monitoring.Subscribers;

/// <summary>
/// Represents the message subscriber service.
/// </summary>
public class MetricsSubscriberService : BaseSubscriberService<MetricMessage, SubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<MetricEntity> _repository;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages subscriber.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to creates <seealso cref="ILogger"/> instances.
    /// </param>
    /// <param name="repository">
    /// The repository to manage the application metrics.
    /// </param>
    public MetricsSubscriberService(IOptions<SubscriberServiceOptions> options,
                                    ILoggerFactory loggerFactory,
                                    IRepository<MetricEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<MetricsSubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(MetricMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new MetricEntity
            {
                AppId = message.AppId,
                Dimension = message.Dimension ?? String.Empty,
                Id = message.Id,
                Name = message.Name,
                OperationId = message.OperationId,
                Timestamp = message.Timestamp,
                Value = message.Value
            });

            _repository.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving the message: {message.Id}");
            _repository.Rollback();
        }
    }
}
