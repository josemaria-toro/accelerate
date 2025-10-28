using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Data;
using Zetatech.Accelerate.Logging.Entities;
using Zetatech.Accelerate.Logging.Messages;
using Zetatech.Accelerate.Messaging;

namespace Zetatech.Accelerate.Monitoring.Subscribers;

/// <summary>
/// Represents the message subscriber service.
/// </summary>
public class TracesSubscriberService : BaseSubscriberService<TraceMessage, SubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<TraceEntity> _repository;

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
    /// The repository to manage the application traces.
    /// </param>
    public TracesSubscriberService(IOptions<SubscriberServiceOptions> options,
                                   ILoggerFactory loggerFactory,
                                   IRepository<TraceEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<TracesSubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(TraceMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new TraceEntity
            {
                AppId = message.AppId,
                Category = message.Category,
                Device = message.Device,
                Event = message.Event ?? String.Empty,
                Id = message.Id,
                Message = message.Message,
                OperatingSystem = message.OperatingSystem,
                OperationId = message.OperationId,
                Platform = message.Platform,
                SeverityLevel = (Int32)message.SeverityLevel,
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
