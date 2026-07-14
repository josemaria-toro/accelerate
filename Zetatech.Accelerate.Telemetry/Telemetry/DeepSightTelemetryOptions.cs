using System;

namespace Zetatech.Accelerate.Telemetry;

internal sealed class DeepSightTelemetryOptions
{
    public Guid Tenant { get; set; }
    public Uri Uri { get; set; }
}
