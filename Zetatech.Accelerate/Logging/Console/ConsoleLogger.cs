using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Abstractions;

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

            System.Console.ForegroundColor = GetConsoleColor(logLevel);

            var scopes = GetScopeInfo();

            foreach (var scope in scopes)
            {
                TrackTrace(logLevel, scope, activity);
            }

            TrackTrace(logLevel, $"{state}", activity);
            TrackExceptions(logLevel, exception, activity);

            System.Console.ResetColor();

            _semaphore.Release();
        }
    }
    private void TrackExceptions(LogLevel logLevel,
                                 Exception exception,
                                 Activity activity)
    {
        while (exception != null)
        {
            System.Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            System.Console.Write($" | ");

            if (activity != null)
            {
                System.Console.Write($"{activity.TraceId}");
                System.Console.Write($" | ");
                System.Console.Write($"{activity.SpanId}");
                System.Console.Write($" | ");
            }

            System.Console.Write($"{Category}");
            System.Console.Write($" | ");
            System.Console.Write($"{logLevel}");
            System.Console.Write($" | ");
            System.Console.Write($"{exception.GetType().Name}");
            System.Console.Write($" | ");
            System.Console.WriteLine(exception.Message);

            System.Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            System.Console.Write($" | ");

            if (activity != null)
            {
                System.Console.Write($"{activity.TraceId}");
                System.Console.Write($" | ");
                System.Console.Write($"{activity.SpanId}");
                System.Console.Write($" | ");
            }

            System.Console.Write($"{Category}");
            System.Console.Write($" | ");
            System.Console.Write($"{logLevel}");
            System.Console.Write($" | ");
            System.Console.Write($"{exception.GetType().Name}");
            System.Console.Write($" | ");
            System.Console.WriteLine(exception.StackTrace);

            exception = exception.InnerException;
        }
    }
    private void TrackTrace(LogLevel logLevel,
                            String message,
                            Activity activity)
    {
        System.Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
        System.Console.Write($" | ");

        if (activity != null)
        {
            System.Console.Write($"{activity.TraceId}");
            System.Console.Write($" | ");
            System.Console.Write($"{activity.SpanId}");
            System.Console.Write($" | ");
        }

        System.Console.Write($"{Category}");
        System.Console.Write($" | ");
        System.Console.Write($"{logLevel}");
        System.Console.Write($" | ");
        System.Console.WriteLine(message);
    }
}
