using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Telemetry.Abstractions;

public abstract class BaseTelemetry : ITelemetry
{
    private Boolean _disposed;

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
    public abstract Task TrackDependencyAsync(String name, String type, String target, Boolean success, Double duration, Byte[] dataInput = null, Byte[] dataOutput = null, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
    public abstract Task TrackEventAsync(String name, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
    public abstract Task TrackMetricAsync(String name, String dimension, Double value, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
    public abstract Task TrackPageViewAsync(String name, String deviceType, Uri uri = null, String userAgent = null, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
    public abstract Task TrackRequestAsync(String name, String endpoint, String type, Boolean success, Double duration, IPAddress ipAddress, Int32 statusCode, Byte[] dataInput = null, Byte[] dataOutput = null, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
    public abstract Task TrackTestResultAsync(String name, Boolean success, Double duration, String message = null, IDictionary<String, Object> metadata = null, CancellationToken cancellationToken = default);
}
