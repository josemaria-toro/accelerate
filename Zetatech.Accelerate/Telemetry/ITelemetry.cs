using System;
using System.Collections.Generic;
using System.Net;

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
    void TrackEvent(String name,
                    IDictionary<String, Object> metadata = null);
    void TrackMetric(String name,
                     String dimension,
                     Double value,
                     IDictionary<String, Object> metadata = null);
    void TrackPageView(String name,
                       String deviceType,
                       Uri uri = null,
                       String userAgent = null,
                       IDictionary<String, Object> metadata = null);
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
    void TrackTestResult(String name,
                         Boolean success,
                         Double duration,
                         String message = null,
                         IDictionary<String, Object> metadata = null);
}