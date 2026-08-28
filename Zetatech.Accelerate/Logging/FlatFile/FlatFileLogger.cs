using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Logging.Abstractions;

namespace Zetatech.Accelerate.Logging.FlatFile;

public sealed class FlatFileLogger : BaseLogger<FlatFileLoggerOptions>
{
    private readonly ChannelWriter<String> _channelWriter;

    public FlatFileLogger(IOptions<FlatFileLoggerOptions> options,
                          String category,
                          ChannelWriter<String> channelWriter) : base(options, category)
    {
        _channelWriter = channelWriter ?? throw new ArgumentException("The provided channel writer must be a valid instance", nameof(channelWriter));
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
            var stringBuilder = new StringBuilder();

            TrackTrace(stringBuilder, logLevel, $"{state}", activity);
            TrackException(stringBuilder, logLevel, exception, activity);

            if (stringBuilder.Length > 0)
            {
                _channelWriter.TryWrite(stringBuilder.ToString());
            }
        }
    }
    private void TrackException(StringBuilder stringBuilder,
                                LogLevel logLevel,
                                Exception exception,
                                Activity activity)
    {
        stringBuilder.Append($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
        stringBuilder.Append($" | ");
        stringBuilder.Append($" E ");
        stringBuilder.Append($" | ");

        if (activity != null)
        {
            stringBuilder.Append($"{activity.TraceId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity.SpanId}");
            stringBuilder.Append($" | ");
        }

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
            TrackException(stringBuilder, logLevel, exception.InnerException, activity);
        }
    }
    private void TrackTrace(StringBuilder stringBuilder,
                            LogLevel logLevel,
                            String message,
                            Activity activity)
    {
        stringBuilder.Append($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff zzz}");
        stringBuilder.Append($" | ");
        stringBuilder.Append($" T ");
        stringBuilder.Append($" | ");

        if (activity != null)
        {
            stringBuilder.Append($"{activity.TraceId}");
            stringBuilder.Append($" | ");
            stringBuilder.Append($"{activity.SpanId}");
            stringBuilder.Append($" | ");
        }

        stringBuilder.Append($"{Category}");
        stringBuilder.Append($" | ");
        stringBuilder.Append($"{logLevel}");
        stringBuilder.Append($" | ");
        stringBuilder.AppendLine(message);
    }
}
