using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Extensions methods for the <see cref="ILoggingProvider"/> interface.
/// </summary>
public static class ILoggingProviderExtensions
{
    /// <summary>
    /// Creates a new <see cref="ILogger"/> instance.
    /// </summary>
    /// <param name="loggingProvider">
    /// The instance of the logging provider.
    /// </param>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    public static async Task<ILogger> CreateLoggerAsync(ILoggingProvider loggingProvider, String categoryName)
    {
        return await Task.Run(() => loggingProvider.CreateLogger(categoryName));
    }
}
