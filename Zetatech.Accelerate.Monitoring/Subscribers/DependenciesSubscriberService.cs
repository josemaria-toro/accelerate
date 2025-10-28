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
public class DependenciesSubscriberService : BaseSubscriberService<DependencyMessage, SubscriberServiceOptions>
{
    private ILogger _logger;
    private IRepository<DependencyEntity> _repository;

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
    /// The repository to manage the dependencies.
    /// </param>
    public DependenciesSubscriberService(IOptions<SubscriberServiceOptions> options,
                                         ILoggerFactory loggerFactory,
                                         IRepository<DependencyEntity> repository) : base(options)
    {
        _logger = loggerFactory.CreateLogger<DependenciesSubscriberService>();
        _repository = repository;
    }

    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public override void Receive(DependencyMessage message)
    {
        _logger.LogInformation($"New message was received: {message.Id}");

        try
        {
            _repository.Insert(new DependencyEntity
            {
                AppId = message.AppId,
                Duration = message.Duration,
                Id = message.Id,
                InputData = message.InputData ?? String.Empty,
                Name = message.Name,
                OperationId = message.OperationId,
                OutputData = message.OutputData ?? String.Empty,
                Success = message.Success,
                Target = message.Target ?? String.Empty,
                Timestamp = message.Timestamp,
                Type = message.Type
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
