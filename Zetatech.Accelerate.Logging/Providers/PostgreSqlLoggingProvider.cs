using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Zetatech.Accelerate.Logging.Services;
using OptionsBuilder = Microsoft.Extensions.Options.Options;

namespace Zetatech.Accelerate.Logging.Providers;

/// <summary>
/// Represents an implementation for a custom PostgreSQL-based logging provider.
/// </summary>
public sealed class PostgreSqlLoggingProvider : BaseLoggingProvider<PostgreSqlLoggingProviderOptions>
{
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">
    /// The logger options to be used by the provider.
    /// </param>
    public PostgreSqlLoggingProvider(IOptions<PostgreSqlLoggingProviderOptions> options) : base(options)
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
            var loggingServiceOptions = new PostgreSqlLoggingServiceOptions
            {
                AppId = Options.AppId,
                ConnectionString = Options.ConnectionString,
                Critical = Options.Critical,
                Debug = Options.Debug,
                Error = Options.Error,
                Information = Options.Information,
                Warning = Options.Warning
            };

            return new PostgreSqlLoggingService(OptionsBuilder.Create(loggingServiceOptions), x);
        });
    }
}