using System;

namespace Accelerate.Telemetry;

/// <summary>
/// Configuration options for AppInsightsTelemetryService telemetry provider.
/// </summary>
public sealed class AppInsightsTelemetryServiceOptions : TelemetryServiceOptions
{
    /// <summary>
    /// Connection string.
    /// </summary>
    public String ConnectionString { get; set; }
}