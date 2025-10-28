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
public class EventsSubscriberService : BaseSubscriberService<EventMessage, SubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<EventEntity> _repository;

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
    /// The repository to manage the application events.
    /// </param>
    public EventsSubscriberService(IOptions<SubscriberServiceOptions> options,
                                   ILoggerFactory loggerFactory,
                                   IRepository<EventEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<EventsSubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(EventMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new EventEntity
            {
                AppId = message.AppId,
                Id = message.Id,
                Name = message.Name,
                OperationId = message.OperationId,
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
