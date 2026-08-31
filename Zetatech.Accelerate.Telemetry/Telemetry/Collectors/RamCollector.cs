using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Jobs.Abstractions;

namespace Zetatech.Accelerate.Telemetry.Collectors;

public sealed class RamCollector : BasePeriodicJob
{
    private readonly Int64 _memoryAvailable;
    private IDictionary<String, Object> _metadata;
    private readonly Process _process;
    private readonly ITelemetry _telemetry;

    public RamCollector(ITelemetry telemetry) : base(TimeSpan.FromSeconds(1), true)
    {
        _memoryAvailable = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
        _process = Process.GetCurrentProcess();
        _metadata = new Dictionary<String, Object>
        {
            { "hostName", _process.MachineName },
            { "processName", _process.ProcessName },
            { "startTime", $"{_process.StartTime.ToUniversalTime():yyyy-MM-dd HH:mm:ss.fffff}" }
        };
        _telemetry = telemetry ?? throw new ArgumentException("The provided telemetry service must be a valid instance", nameof(telemetry));
    }

    protected override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        _process.Refresh();

        var ramUsagePercent = _memoryAvailable > 0 ? _process.WorkingSet64 / (Double)_memoryAvailable * 100.0 : 0;

        await _telemetry.TrackMetricAsync("RAM", "Usage %", ramUsagePercent, _metadata, cancellationToken)
                        .ConfigureAwait(false);
    }
}
