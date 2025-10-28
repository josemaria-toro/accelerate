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
public class ErrorsSubscriberService : BaseSubscriberService<ErrorMessage, SubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<ErrorEntity> _repository;

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
    /// The repository to manage the application errors.
    /// </param>
    public ErrorsSubscriberService(IOptions<SubscriberServiceOptions> options,
                                   ILoggerFactory loggerFactory,
                                   IRepository<ErrorEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<ErrorsSubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(ErrorMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new ErrorEntity
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
                StackTrace = message.StackTrace,
                Timestamp = message.Timestamp,
                TypeName = message.TypeName
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
