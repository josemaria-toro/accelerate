using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Extensions methods for the <see cref="ILoggingFactory"/> interface.
/// </summary>
public static class ILoggingFactoryExtensions
{
    /// <summary>
    /// Creates a new <see cref="ILogger"/> instance.
    /// </summary>
    /// <param name="loggingFactory">
    /// The instance of the logging factory.
    /// </param>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    public static async Task<ILogger> CreateLoggerAsync(ILoggingFactory loggingFactory, String categoryName)
    {
        return await Task.Run(() => loggingFactory.CreateLogger(categoryName));
    }
    /// <summary>
    /// Adds an <see cref="ILoggerProvider"/> to the logging system.
    /// </summary>
    /// <param name="loggingFactory">
    /// The instance of the logging factory.
    /// </param>
    /// <param name="loggerProvider">
    /// The <see cref="ILoggerProvider"/>.
    /// </param>
    public static async Task AddProvider(ILoggingFactory loggingFactory, ILoggerProvider loggerProvider)
    {
        await Task.Run(() => loggingFactory.AddProvider(loggerProvider));
    }
}
