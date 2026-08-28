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
    private readonly ChannelWriter<FlatFileLoggerEntry> _channelWriter;

    public FlatFileLogger(IOptions<FlatFileLoggerOptions> options,
                          String category,
                          ChannelWriter<FlatFileLoggerEntry> channelWriter) : base(options, category)
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

            TrackTrace(stringBuilder, logLevel, $"{state}", activity);
            TrackException(stringBuilder, logLevel, exception, activity);

            if (stringBuilder.Length > 0)
            {
                await _channelWriter.WriteAsync(new FlatFileLoggerEntry
                {
                    Message = stringBuilder.ToString()
                });
            }
        }
    }
    private void TrackException(StringBuilder stringBuilder,
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
                TrackException(stringBuilder, logLevel, exception.InnerException, activity);
            }
        }
    }
    private void TrackTrace(StringBuilder stringBuilder,
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
