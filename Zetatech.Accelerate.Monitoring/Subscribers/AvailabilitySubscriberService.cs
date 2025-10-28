using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Messaging;
using Zetatech.Accelerate.Messaging.Subscribers;
using Zetatech.Accelerate.Telemetry.Entities;
using Zetatech.Accelerate.Telemetry.Messages;

namespace Zetatech.Accelerate.Monitoring.Subscribers;

/// <summary>
/// Represents the message subscriber service.
/// </summary>
public class AvailabilitySubscriberService : RabbitMqSubscriberService<AvailabilityMessage, RabbitMqSubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<AvailabilityEntity> _repository;

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
    /// The repository to manage the results of the availability tests.
    /// </param>
    public AvailabilitySubscriberService(IOptions<SubscriberServiceOptions> options,
                                         ILoggerFactory loggerFactory,
                                         IRepository<AvailabilityEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<AvailabilitySubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(AvailabilityMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new AvailabilityEntity
            {
                AppId = message.AppId,
                Duration = message.Duration,
                Id = message.Id,
                Message = message.Message,
                Name = message.Name,
                OperationId = message.OperationId,
                Success = message.Success,
                Timestamp = message.Timestamp
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
