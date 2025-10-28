using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Logging.Services;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Providers;

/// <summary>
/// Represents an implementation for a custom console-based logging provider.
/// </summary>
public sealed class ConsoleLoggingProvider : BaseLoggingProvider<ConsoleLoggingProviderOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    public ConsoleLoggingProvider(IOptions<ConsoleLoggingProviderOptions> options) : base(options)
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
            var loggingServiceOptions = new ConsoleLoggingServiceOptions
            {
                AppId = Options.AppId,
                Critical = Options.Critical,
                Debug = Options.Debug,
                Error = Options.Error,
                Information = Options.Information,
                Warning = Options.Warning
            };

            return new ConsoleLoggingService(OptionsBuilder.Create(loggingServiceOptions), x);
        });
    }
}