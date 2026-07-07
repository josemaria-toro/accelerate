using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Telemetry;

public interface ITelemetry : IDisposable
{
    void TrackDependency(String name,
                         String type,
                         String target,
                         Boolean success,
                         Double duration,
                         Byte[] dataInput = null,
                         Byte[] dataOutput = null,
                         IDictionary<String, Object> metadata = null);
    Task TrackDependencyAsync(String name,
                              String type,
                              String target,
                              Boolean success,
                              Double duration,
                              Byte[] dataInput = null,
                              Byte[] dataOutput = null,
                              IDictionary<String, Object> metadata = null,
                              CancellationToken cancellationToken = default);
    void TrackEvent(String name,
                    IDictionary<String, Object> metadata = null);
    Task TrackEventAsync(String name,
                         IDictionary<String, Object> metadata = null,
                         CancellationToken cancellationToken = default);
    void TrackMetric(String name,
                     String dimension,
                     Double value,
                     IDictionary<String, Object> metadata = null);
    Task TrackMetricAsync(String name,
                          String dimension,
                          Double value,
                          IDictionary<String, Object> metadata = null,
                          CancellationToken cancellationToken = default);
    void TrackPageView(String name,
                       String deviceType,
                       Uri uri = null,
                       String userAgent = null,
                       IDictionary<String, Object> metadata = null);
    Task TrackPageViewAsync(String name,
                            String deviceType,
                            Uri uri = null,
                            String userAgent = null,
                            IDictionary<String, Object> metadata = null,
                            CancellationToken cancellationToken = default);
    void TrackRequest(String name,
                      String endpoint,
                      String type,
                      Boolean success,
                      Double duration,
                      IPAddress ipAddress,
                      Int32 statusCode,
                      Byte[] dataInput = null,
                      Byte[] dataOutput = null,
                      IDictionary<String, Object> metadata = null);
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
    void TrackTestResult(String name,
                         Boolean success,
                         Double duration,
                         String message = null,
                         IDictionary<String, Object> metadata = null);
    Task TrackTestResultAsync(String name,
                              Boolean success,
                              Double duration,
                              String message = null,
                              IDictionary<String, Object> metadata = null,
                              CancellationToken cancellationToken = default);
}