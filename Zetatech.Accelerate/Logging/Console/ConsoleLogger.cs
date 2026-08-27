using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Abstractions;

using Shell = System.Console;

namespace Zetatech.Accelerate.Logging.Console;

public sealed class ConsoleLogger : BaseLogger<ConsoleLoggerOptions>
{
    private readonly SemaphoreSlim _semaphore;

    public ConsoleLogger(IOptions<ConsoleLoggerOptions> options,
                         String category,
                         SemaphoreSlim semaphore) : base(options, category)
    {
        _semaphore = semaphore ?? throw new ArgumentException("The provided semaphore must be a valid instance", nameof(semaphore));
    }

    private static ConsoleColor GetConsoleColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Critical => ConsoleColor.DarkRed,
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Warning => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };
    }
    public override void Log<TState>(LogLevel logLevel,
                                     EventId eventId,
                                     TState state,
                                     Exception exception,
                                     Func<TState, Exception, String> formatter)
    {
        if (IsEnabled(logLevel))
        {
            var activity = Activity.Current;

            _semaphore.Wait();

            Shell.ForegroundColor = GetConsoleColor(logLevel);

            TrackTrace(logLevel, $"{state}", activity);
            TrackExceptions(logLevel, exception, activity);

            Shell.ResetColor();

            _semaphore.Release();
        }
    }
    private void TrackExceptions(LogLevel logLevel,
                                 Exception exception,
                                 Activity activity)
    {
        while (exception != null)
        {
            Shell.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            Shell.Write($" | ");

            if (activity != null)
            {
                Shell.Write($"{activity.TraceId}");
                Shell.Write($" | ");
                Shell.Write($"{activity.SpanId}");
                Shell.Write($" | ");
            }

            Shell.Write($"{Category}");
            Shell.Write($" | ");
            Shell.Write($"{logLevel}");
            Shell.Write($" | ");
            Shell.Write($"{exception.GetType().Name}");
            Shell.Write($" | ");
            Shell.WriteLine(exception.Message);

            Shell.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            Shell.Write($" | ");

            if (activity != null)
            {
                Shell.Write($"{activity.TraceId}");
                Shell.Write($" | ");
                Shell.Write($"{activity.SpanId}");
                Shell.Write($" | ");
            }

            Shell.Write($"{Category}");
            Shell.Write($" | ");
            Shell.Write($"{logLevel}");
            Shell.Write($" | ");
            Shell.Write($"{exception.GetType().Name}");
            Shell.Write($" | ");
            Shell.WriteLine(exception.StackTrace);

            exception = exception.InnerException;
        }
    }
    private void TrackTrace(LogLevel logLevel,
                            String message,
                            Activity activity)
    {
        Shell.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
        Shell.Write($" | ");

        if (activity != null)
        {
            Shell.Write($"{activity.TraceId}");
            Shell.Write($" | ");
            Shell.Write($"{activity.SpanId}");
            Shell.Write($" | ");
        }

        Shell.Write($"{Category}");
        Shell.Write($" | ");
        Shell.Write($"{logLevel}");
        Shell.Write($" | ");
        Shell.WriteLine(message);
    }
}
