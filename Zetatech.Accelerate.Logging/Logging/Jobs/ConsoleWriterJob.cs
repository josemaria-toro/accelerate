using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zetatech.Accelerate.Jobs.Abstractions;
using Zetatech.Accelerate.Logging.ChannelEntries;

namespace Zetatech.Accelerate.Logging.Jobs;

internal sealed class ConsoleWriterJob : BaseJob
{
    private readonly Channel<ConsoleChannelEntry> _channel;

    public ConsoleWriterJob(Channel<ConsoleChannelEntry> channel)
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
                await foreach (var channelEntry in _channel.Reader.ReadAllAsync(cancellationToken)
                                                                  .ConfigureAwait(false))
                {
                    WriteChannelEntry(channelEntry);
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            while (_channel.Reader.TryRead(out var channelEntry))
            {
                WriteChannelEntry(channelEntry);
            }
        }
    }
    private static void WriteChannelEntry(ConsoleChannelEntry channelEntry)
    {
        Console.ForegroundColor = channelEntry.Severity switch
        {
            LogLevel.Critical => ConsoleColor.Magenta,
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Warning => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };

        Console.Write(channelEntry.Message);
        Console.ResetColor();
    }
}
