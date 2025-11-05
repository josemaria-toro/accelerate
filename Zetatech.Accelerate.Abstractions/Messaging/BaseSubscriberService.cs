using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Zetatech.Accelerate.Messaging;

/// <summary>
/// Represents a base class for implementing custom subscribers for receiving messages from a publisher.
/// </summary>
/// <typeparam name="TMessage">
/// The type of message to be received.
/// </typeparam>
/// <typeparam name="TOptions">
/// The type of the configuration options for the subscriber. Must inherit from <see cref="BaseSubscriberServiceOptions"/>.
/// </typeparam>
public abstract class BaseSubscriberService<TMessage, TOptions> : BaseDisposable, ISubscriberService<TMessage> where TMessage : BaseMessage
                                                                                                               where TOptions : BaseSubscriberServiceOptions
{
    private Boolean _disposed;
    private ILogger _logger;
    private TOptions _options;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the messages subscriber.
    /// </param>
    /// <param name="loggerFactory">
    /// The factory to create instances of loggers.
    /// </param>
    protected BaseSubscriberService(IOptions<TOptions> options, ILoggerFactory loggerFactory = null)
    {
        _logger = loggerFactory?.CreateLogger(GetType().Name);
        _options = options?.Value;
    }

    /// <summary>
    /// Gets the instance of the logger.
    /// </summary>
    protected ILogger Logger => _logger;
    /// <summary>
    /// Gets the options for the messages subscriber.
    /// </summary>
    protected TOptions Options => _options;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method is called from Dispose or the finalizer.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;

        base.Dispose(disposing);

        if (disposing)
        {
            _logger = null;
            _options = null;
        }
    }
    /// <summary>
    /// Receives a message from the publisher.
    /// </summary>
    /// <param name="message">
    /// The message to receive.
    /// </param>
    public abstract void Receive(TMessage message);
}