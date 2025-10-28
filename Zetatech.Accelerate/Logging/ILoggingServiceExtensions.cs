using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Logging;

/// <summary>
/// Extensions methods for the <see cref="ILoggingService"/> interface.
/// </summary>
public static class ILoggingServiceExtensions
{
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of the object to be written.
    /// </typeparam>
    /// <param name="loggingService">
    /// The instance of the logging service.
    /// </param>
    /// <param name="logLevel">
    /// Entry will be written on this level.
    /// </param>
    /// <param name="eventId">
    /// Id of the event.
    /// </param>
    /// <param name="state">
    /// The entry to be written. Can be also an object.
    /// </param>
    /// <param name="exception">
    /// The exception related to this entry.
    /// </param>
    /// <param name="formatter">
    /// Function to create a <see cref="String"/> message of the <paramref name="state"/> and <paramref name="exception"/>.
    /// </param>
    public static async Task LogAsync<TState>(this ILoggingService loggingService, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
    {
        await Task.Run(() => loggingService.Log(logLevel, eventId, state, exception, formatter));
    }
    /// <summary>
    /// Checks if the given <paramref name="logLevel"/> is enabled.
    /// </summary>
    /// <param name="loggingService">
    /// The instance of the logging service.
    /// </param>
    /// <param name="logLevel">
    /// Level to be checked.
    /// </param>
    public static async Task<Boolean> IsEnabledAsync(this ILoggingService loggingService, LogLevel logLevel)
    {
        return await Task.Run(() => loggingService.IsEnabledAsync(logLevel));
    }
    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of the state to begin scope for.
    /// </typeparam>
    /// <param name="loggingService">
    /// The instance of the logging service.
    /// </param>
    /// <param name="state">
    /// The identifier for the scope.
    /// </param>
    public static async Task<IDisposable> BeginScopeAsync<TState>(this ILoggingService loggingService, TState state)
    {
        return await Task.Run(() => loggingService.BeginScopeAsync(state));
    }
}
