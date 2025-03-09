﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

using OptionsCreator = Microsoft.Extensions.Options.Options;

namespace Accelerate.Logging;

/// <summary>
/// Represent a type that can create instances of type ILogger based on file system.
/// </summary>
[ProviderAlias("FileSystem")]
public sealed class FileSystemLoggerProvider : LoggerProvider<FileSystemLoggerServiceOptions>
{
    private Boolean _disposed;
    private ConcurrentDictionary<String, ILogger> _loggerServices;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="options">
    /// Configuration options for logger provider.
    /// </param>
    public FileSystemLoggerProvider(IOptions<FileSystemLoggerServiceOptions> options) : base(options)
    {
        _loggerServices = new ConcurrentDictionary<String, ILogger>();
    }

    /// <summary>
    /// Creates a new instance of logger.
    /// </summary>
    /// <param name="categoryName">
    /// The category name for messages produced by the logger.
    /// </param>
    public override ILogger CreateLogger(String categoryName)
    {
        if (String.IsNullOrEmpty(categoryName))
        {
            throw new ArgumentException("The category cannot be null or empty", nameof(categoryName));
        }

        var options = OptionsCreator.Create(Options);

        return _loggerServices.GetOrAdd(categoryName, _ => new FileSystemLoggerService(options, categoryName));
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
    /// </param>
    protected override void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        base.Dispose(disposing);

        if (disposing)
        {
            _loggerServices = null;
        }

        _disposed = true;
    }
}