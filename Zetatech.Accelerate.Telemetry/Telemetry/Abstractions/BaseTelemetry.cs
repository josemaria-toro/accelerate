using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Telemetry.Abstractions;

public abstract class BaseTelemetry : ITelemetry
{
    private Boolean _disposed;
    private readonly ILogger _logger;

    protected BaseTelemetry(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType().Name);
    }

    protected ILogger Logger => _logger;

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
    public async Task TrackDependencyAsync(String name,
                                           String type,
                                           String target,
                                           Boolean success,
                                           Double duration,
                                           Byte[] dataInput = null,
                                           Byte[] dataOutput = null,
                                           IDictionary<String, Object> metadata = null,
                                           CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackDependency(name, type, target, success, duration, dataInput, dataOutput, metadata), cancellationToken);
    }
    public abstract void TrackEvent(String name,
                                    IDictionary<String, Object> metadata = null);
    public async Task TrackEventAsync(String name,
                                      IDictionary<String, Object> metadata = null,
                                      CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackEvent(name, metadata), cancellationToken);
    }
    public abstract void TrackMetric(String name,
                                     String dimension,
                                     Double value,
                                     IDictionary<String, Object> metadata = null);
    public async Task TrackMetricAsync(String name,
                                       String dimension,
                                       Double value,
                                       IDictionary<String, Object> metadata = null,
                                       CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackMetric(name, dimension, value, metadata), cancellationToken);
    }
    public abstract void TrackPageView(String name,
                                       String deviceType,
                                       Uri uri = null,
                                       String userAgent = null,
                                       IDictionary<String, Object> metadata = null);
    public async Task TrackPageViewAsync(String name,
                                         String deviceType,
                                         Uri uri = null,
                                         String userAgent = null,
                                         IDictionary<String, Object> metadata = null,
                                         CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackPageView(name, deviceType, uri, userAgent, metadata), cancellationToken);
    }
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
    public async Task TrackRequestAsync(String name,
                                        String endpoint,
                                        String type,
                                        Boolean success,
                                        Double duration,
                                        IPAddress ipAddress,
                                        Int32 statusCode,
                                        Byte[] dataInput = null,
                                        Byte[] dataOutput = null,
                                        IDictionary<String, Object> metadata = null,
                                        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackRequest(name, endpoint, type, success, duration, ipAddress, statusCode, dataInput, dataOutput, metadata), cancellationToken);
    }
    public abstract void TrackTestResult(String name,
                                         Boolean success,
                                         Double duration,
                                         String message = null,
                                         IDictionary<String, Object> metadata = null);
    public async Task TrackTestResultAsync(String name,
                                           Boolean success,
                                           Double duration,
                                           String message = null,
                                           IDictionary<String, Object> metadata = null,
                                           CancellationToken cancellationToken = default)
    {
        await Task.Run(() => TrackTestResult(name, success, duration, message, metadata), cancellationToken);
    }
}