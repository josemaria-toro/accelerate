using System;
using System.Threading;

namespace Zetatech.Accelerate.Logging;

internal sealed class LoggerScope : IDisposable
{
    private static readonly AsyncLocal<LoggerScope> _current = new();
    private Boolean _disposed;
    private LoggerScope _parent;
    private Object _state;

    public LoggerScope(Object state)
    {
        _parent = _current.Value;
        _current.Value = this;
        _state = state;
    }

    public static LoggerScope Current => _current.Value;
    public LoggerScope Parent => _parent;
    public Object State => _state;

    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _current.Value = _parent;
        _state = null;

        GC.SuppressFinalize(this);
    }
}
