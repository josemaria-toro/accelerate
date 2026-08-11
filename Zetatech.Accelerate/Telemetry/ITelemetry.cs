using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Telemetry;

public interface ITelemetry : IDisposable
{
    Task TrackDependencyAsync(String name,
                              String type,
                              String target,
                              Boolean success,
                              Double duration,
                              Byte[] dataInput = null,
                              Byte[] dataOutput = null,
                              IDictionary<String, Object> metadata = null,
                              CancellationToken cancellationToken = default);
    Task TrackEventAsync(String name,
                         IDictionary<String, Object> metadata = null,
                         CancellationToken cancellationToken = default);
    Task TrackMetricAsync(String name,
                          String dimension,
                          Double value,
                          IDictionary<String, Object> metadata = null,
                          CancellationToken cancellationToken = default);
    Task TrackPageViewAsync(String name,
                            String deviceType,
                            Uri uri = null,
                            String userAgent = null,
                            IDictionary<String, Object> metadata = null,
                            CancellationToken cancellationToken = default);
    Task TrackRequestAsync(String name,
                           String endpoint,
                           String type,
                           Boolean success,
                           Double duration,
                           IPAddress ipAddress,
                           Int32 statusCode,
                           Byte[] dataInput = null,
                           Byte[] dataOutput = null,
                           IDictionary<String, Object> metadata = null,
                           CancellationToken cancellationToken = default);
    Task TrackTestResultAsync(String name,
                              Boolean success,
                              Double duration,
                              String message = null,
                              IDictionary<String, Object> metadata = null,
                              CancellationToken cancellationToken = default);
}