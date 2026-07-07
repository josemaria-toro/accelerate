using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Abstractions;

namespace Zetatech.Accelerate.Logging;

internal sealed class ConsoleLogger : BaseLogger<ConsoleLoggerOptions>
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

            Console.ForegroundColor = GetConsoleColor(logLevel);

            var scopes = GetScopeInfo();

            foreach (var scope in scopes)
            {
                TrackTrace(logLevel, scope, activity);
            }

            TrackTrace(logLevel, $"{state}", activity);
            TrackExceptions(logLevel, exception, activity);

            Console.ResetColor();

            _semaphore.Release();
        }
    }
    private void TrackExceptions(LogLevel logLevel,
                                 Exception exception,
                                 Activity activity)
    {
        while (exception != null)
        {
            Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            Console.Write($" | ");

            if (activity != null)
            {
                Console.Write($"{activity.TraceId}");
                Console.Write($" | ");
                Console.Write($"{activity.SpanId}");
                Console.Write($" | ");
            }

            Console.Write($"{Category}");
            Console.Write($" | ");
            Console.Write($"{logLevel}");
            Console.Write($" | ");
            Console.Write($"{exception.GetType().Name}");
            Console.Write($" | ");
            Console.WriteLine(exception.Message);

            Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
            Console.Write($" | ");

            if (activity != null)
            {
                Console.Write($"{activity.TraceId}");
                Console.Write($" | ");
                Console.Write($"{activity.SpanId}");
                Console.Write($" | ");
            }

            Console.Write($"{Category}");
            Console.Write($" | ");
            Console.Write($"{logLevel}");
            Console.Write($" | ");
            Console.Write($"{exception.GetType().Name}");
            Console.Write($" | ");
            Console.WriteLine(exception.StackTrace);

            exception = exception.InnerException;
        }
    }
    private void TrackTrace(LogLevel logLevel,
                            String message,
                            Activity activity)
    {
        Console.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
        Console.Write($" | ");

        if (activity != null)
        {
            Console.Write($"{activity.TraceId}");
            Console.Write($" | ");
            Console.Write($"{activity.SpanId}");
            Console.Write($" | ");
        }

        Console.Write($"{Category}");
        Console.Write($" | ");
        Console.Write($"{logLevel}");
        Console.Write($" | ");
        Console.WriteLine(message);
    }
}
