using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Jobs.Abstractions;

using Shell = System.Console;

namespace Zetatech.Accelerate.Logging.Console;

public sealed class ConsoleLoggerJob : BaseJob
{
    private readonly Channel<ConsoleLoggerEntry> _channel;

    public ConsoleLoggerJob(Channel<ConsoleLoggerEntry> channel)
    {
        _channel = channel ?? throw new ArgumentException("The provided channel must be a valid instance", nameof(channel));
    }

    protected override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (await _channel.Reader.WaitToReadAsync(cancellationToken)
                                        .ConfigureAwait(false))
            {
                await foreach (var loggerEntry in _channel.Reader.ReadAllAsync(cancellationToken)
                                                                 .ConfigureAwait(false))
                {
                    WriteLoggerEntry(loggerEntry);
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            while (_channel.Reader.TryRead(out var loggerEntry))
            {
                WriteLoggerEntry(loggerEntry);
            }
        }
    }
    private static void WriteLoggerEntry(ConsoleLoggerEntry loggerEntry)
    {
        Shell.ForegroundColor = loggerEntry.Severity switch
        {
            LogLevel.Critical => ConsoleColor.Magenta,
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Warning => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };

        Shell.Write(loggerEntry.Message);
        Shell.ResetColor();
    }
}
