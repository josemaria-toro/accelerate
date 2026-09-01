using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Logging.Abstractions;

public abstract class BaseLogger<TOptions> : ILogger, IDisposable where TOptions : BaseLoggerOptions
{
    private readonly String _category;
    private Boolean _disposed;
    private readonly TOptions _options;

    protected BaseLogger(IOptions<TOptions> options,
                         String category)
    {
        if (String.IsNullOrEmpty(category))
        {
            throw new ArgumentException("The provided category is invalid", nameof(category));
        }

        _category = category;
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));
    }

    protected String Category => _category;
    protected TOptions Options => _options;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return new LoggerScope(state);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(Boolean disposing)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
    }
    protected static IList<String> GetScopeInfo()
    {
        var scope = LoggerScope.Current;

        if (scope == null)
            return [];

        var scopes = new List<String>();

        while (scope != null)
        {
            scopes.Add(scope.State.ToString());
            scope = scope.Parent;
        }

        scopes.Reverse();

        return scopes;
    }
    public Boolean IsEnabled(LogLevel logLevel)
    {
        return logLevel >= Options.LogLevel;
    }
    public abstract void Log<TState>(LogLevel logLevel,
                                     EventId eventId,
                                     TState state,
                                     Exception exception,
                                     Func<TState, Exception, String> formatter);
}
