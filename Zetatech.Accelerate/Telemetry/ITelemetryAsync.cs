using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Telemetry;

public static class ITelemetryAsync
{
    public static async Task TrackDependencyAsync(this ITelemetry telemetry,
                                                  String name,
                                                  String type,
                                                  String target,
                                                  Boolean success,
                                                  Double duration,
                                                  Byte[] dataInput = null,
                                                  Byte[] dataOutput = null,
                                                  IDictionary<String, Object> metadata = null,
                                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => telemetry.TrackDependency(name, type, target, success, duration, dataInput, dataOutput, metadata), cancellationToken);
    }
    public static async Task TrackEventAsync(this ITelemetry telemetry,
                                                  String name,
                                                  IDictionary<String, Object> metadata = null,
                                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => telemetry.TrackEvent(name, metadata), cancellationToken);
    }
    public static async Task TrackMetricAsync(this ITelemetry telemetry,
                                              String name,
                                              String dimension,
                                              Double value,
                                              IDictionary<String, Object> metadata = null,
                                              CancellationToken cancellationToken = default)
    {
        await Task.Run(() => telemetry.TrackMetric(name, dimension, value, metadata), cancellationToken);
    }
    public static async Task TrackPageViewAsync(this ITelemetry telemetry,
                                                String name,
                                                String deviceType,
                                                Uri uri = null,
                                                String userAgent = null,
                                                IDictionary<String, Object> metadata = null,
                                                CancellationToken cancellationToken = default)
    {
        await Task.Run(() => telemetry.TrackPageView(name, deviceType, uri, userAgent, metadata), cancellationToken);
    }
    public static async Task TrackRequestAsync(this ITelemetry telemetry,
                                               String name,
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
        await Task.Run(() => telemetry.TrackRequest(name, endpoint, type, success, duration, ipAddress, statusCode, dataInput, dataOutput, metadata), cancellationToken);
    }
    public static async Task TrackTestResultAsync(this ITelemetry telemetry,
                                                  String name,
                                                  Boolean success,
                                                  Double duration,
                                                  String message = null,
                                                  IDictionary<String, Object> metadata = null,
                                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => telemetry.TrackTestResult(name, success, duration, message, metadata), cancellationToken);
    }
}