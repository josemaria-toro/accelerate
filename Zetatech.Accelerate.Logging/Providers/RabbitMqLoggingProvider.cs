using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Logging.Services;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Providers;

/// <summary>
/// Represents an implementation for a custom RabbitMQ-based logging provider.
/// </summary>
public sealed class RabbitMqLoggingProvider : BaseLoggingProvider<RabbitMqLoggingProviderOptions>
{
    private Boolean _disposed;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    public RabbitMqLoggingProvider(IOptions<RabbitMqLoggingProviderOptions> options) : base(options)
    {
    }

    /// <summary>
    /// Creates a new logger instance for the specified category.
    /// </summary>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    /// <returns>
    /// An <see cref="ILogger"/> instance.
    /// </returns>
    public override ILogger CreateLogger(String categoryName)
    {
        if (String.IsNullOrEmpty(categoryName))
        {
            throw new ArgumentException("The category cannot be null or empty", nameof(categoryName));
        }

        return Loggers.GetOrAdd(categoryName, x =>
        {
            var loggingServiceOptions = new RabbitMqLoggingServiceOptions
            {
                AppId = Options.AppId,
                ConnectionString = Options.ConnectionString,
                Critical = Options.Critical,
                Debug = Options.Debug,
                Error = Options.Error,
                ErrorsRk = Options.ErrorsRk,
                Exchange = Options.Exchange,
                Information = Options.Information,
                TracesRk = Options.TracesRk,
                Warning = Options.Warning
            };

            return new RabbitMqLoggingService(OptionsBuilder.Create(loggingServiceOptions), x);
        });
    }
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
    }
}