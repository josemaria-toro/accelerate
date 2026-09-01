using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Abstractions;
using Zetatech.Accelerate.Logging.ChannelEntries;

namespace Zetatech.Accelerate.Logging.Loggers;

internal sealed class ConsoleLogger : BaseLogger<ConsoleLoggerOptions>
{
    private readonly ChannelWriter<ConsoleChannelEntry> _channelWriter;

    public ConsoleLogger(IOptions<ConsoleLoggerOptions> options,
                         String category,
                         ChannelWriter<ConsoleChannelEntry> channelWriter) : base(options, category)
    {
        _channelWriter = channelWriter ?? throw new ArgumentException("The provided channel writer must be a valid instance", nameof(channelWriter));
    }

    public override async void Log<TState>(LogLevel logLevel,
                                           EventId eventId,
                                           TState state,
                                           Exception exception,
                                           Func<TState, Exception, String> formatter)
    {
        if (IsEnabled(logLevel))
        {
            var activity = Activity.Current;
            var stringBuilder = new StringBuilder();

            AppendTraceData(stringBuilder, logLevel, $"{state}", activity);
            AppendErrorData(stringBuilder, logLevel, exception, activity);

            if (stringBuilder.Length > 0)
            {
                var consoleChannelEntry = new ConsoleChannelEntry
                {
                    Message = stringBuilder.ToString(),
                    Severity = logLevel
                };

                await _channelWriter.WriteAsync(consoleChannelEntry)
                                    .ConfigureAwait(false);
            }
        }
    }
    private void AppendErrorData(StringBuilder stringBuilder,
                                 LogLevel logLevel,
                                 Exception exception,
                                 Activity activity)
    {
        if (exception != null)
        {
            stringBuilder.Append($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fffff zzz}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($" E ");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity?.TraceId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity?.SpanId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{Category}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{logLevel}");
            stringBuilder.Append($" | ");
            stringBuilder.Append(exception.Message);
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{exception.GetType().Name}");
            stringBuilder.Append($" | ");
            stringBuilder.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                AppendErrorData(stringBuilder, logLevel, exception.InnerException, activity);
            }
        }
    }
    private void AppendTraceData(StringBuilder stringBuilder,
                                 LogLevel logLevel,
                                 String message,
                                 Activity activity)
    {
        if (!String.IsNullOrEmpty(message))
        {
            stringBuilder.Append($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fffff zzz}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($" T ");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity?.TraceId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity?.SpanId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{Category}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{logLevel}");
            stringBuilder.Append($" | ");
            stringBuilder.AppendLine(message);
        }
    }
}
