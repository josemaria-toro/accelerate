using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Jobs.Abstractions;

namespace Zetatech.Accelerate.Logging.FlatFile;

public sealed class FlatFileLoggerJob : BaseJob
{
    private readonly Channel<String> _channel;
    private readonly FlatFileLoggerOptions _options;


    public FlatFileLoggerJob(IOptions<FlatFileLoggerOptions> options,
                             Channel<String> channel)
    {
        _channel = channel ?? throw new ArgumentException("The provided channel must be a valid instance", nameof(channel));
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
    }

    protected override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        var fileName = Path.Combine(_options.Path, $"{_options.FileName}.log");

        if (!Directory.Exists(_options.Path))
        {
            Directory.CreateDirectory(_options.Path);
        }

        var stream = new StreamWriter(fileName, true, Encoding.UTF8);

        try
        {
            while (await _channel.Reader.WaitToReadAsync(cancellationToken)
                                        .ConfigureAwait(false))
            {
                await foreach (var contents in _channel.Reader.ReadAllAsync(cancellationToken)
                                                              .ConfigureAwait(false))
                {
                    RotateFileIfNeeded(ref stream, fileName);

                    await stream.WriteAsync(contents)
                                .ConfigureAwait(false);
                    await stream.FlushAsync(cancellationToken)
                                .ConfigureAwait(false);
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            while (_channel.Reader.TryRead(out var contents))
            {
                RotateFileIfNeeded(ref stream, fileName);

                await stream.WriteAsync(contents)
                            .ConfigureAwait(false);
                await stream.FlushAsync(cancellationToken)
                            .ConfigureAwait(false);
            }

            stream.Close();
        }
    }
    private void RotateFileIfNeeded(ref StreamWriter stream, String fileName)
    {
        var maxFileSize = _options.MaxSize * 1024 * 1024;

        if (stream.BaseStream.Length >= maxFileSize)
        {
            stream.Close();

            var backupIndex = 1;
            var backupSuffix = $"{DateTime.UtcNow:yyyyMMdd}";
            var backupFileName = Path.Combine(_options.Path, $"{_options.FileName}_{backupSuffix}_{backupIndex}.log");

            while (File.Exists(backupFileName))
            {
                backupFileName = Path.Combine(_options.Path, $"{_options.FileName}_{backupSuffix}_{++backupIndex}.log");
            }

            File.Move(fileName, backupFileName, false);

            stream = new StreamWriter(fileName, true, Encoding.UTF8);
        }
    }
}