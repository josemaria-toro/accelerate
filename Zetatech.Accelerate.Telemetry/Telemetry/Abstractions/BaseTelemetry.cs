using System;
using System.Collections.Generic;
using System.Net;

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
    public abstract void TrackDependency(String name,
                                         String type,
                                         String target,
                                         Boolean success,
                                         Double duration,
                                         Byte[] dataInput = null,
                                         Byte[] dataOutput = null,
                                         IDictionary<String, Object> metadata = null);
    public abstract void TrackEvent(String name,
                                    IDictionary<String, Object> metadata = null);
    public abstract void TrackMetric(String name,
                                     String dimension,
                                     Double value,
                                     IDictionary<String, Object> metadata = null);
    public abstract void TrackPageView(String name,
                                       String deviceType,
                                       Uri uri = null,
                                       String userAgent = null,
                                       IDictionary<String, Object> metadata = null);
    public abstract void TrackRequest(String name,
                                      String endpoint,
                                      String type,
                                      Boolean success,
                                      Double duration,
                                      IPAddress ipAddress,
                                      Int32 statusCode,
                                      Byte[] dataInput = null,
                                      Byte[] dataOutput = null,
                                      IDictionary<String, Object> metadata = null);
    public abstract void TrackTestResult(String name,
                                         Boolean success,
                                         Double duration,
                                         String message = null,
                                         IDictionary<String, Object> metadata = null);
}