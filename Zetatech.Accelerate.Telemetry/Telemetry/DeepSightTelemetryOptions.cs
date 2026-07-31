using System;

namespace Zetatech.Accelerate.Telemetry;

public sealed class DeepSightTelemetryOptions
{
    public String AppName { get; set; }
    public Version AppVersion { get; set; }
    public Guid Tenant { get; set; }
    public Uri Uri { get; set; }
}
