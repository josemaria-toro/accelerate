using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Zetatech.Accelerate.Jobs.Abstractions;

namespace Zetatech.Accelerate.Telemetry.Collectors;

public sealed class CpuCollector : BasePeriodicJob
{
    private TimeSpan _lastCpuTime;
    private DateTime _lastSampleTime;
    private IDictionary<String, Object> _metadata;
    private Process _process;
    private readonly Int32 _processorCount;
    private readonly ITelemetry _telemetry;

    public CpuCollector(ITelemetry telemetry) : base(TimeSpan.FromSeconds(1), true)
    {
        _process = Process.GetCurrentProcess();
        _lastCpuTime = _process.TotalProcessorTime;
        _lastSampleTime = DateTime.UtcNow;
        _metadata = new Dictionary<String, Object>
        {
            { "hostName", _process.MachineName },
            { "processName", _process.ProcessName },
            { "startTime", $"{_process.StartTime.ToUniversalTime():yyyy-MM-dd HH:mm:ss.fffff}" }
        };
        _processorCount = Environment.ProcessorCount;
        _telemetry = telemetry ?? throw new ArgumentException("The provided telemetry service must be a valid instance", nameof(telemetry));
    }

    protected override async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        _process.Refresh();

        var currentCpuTime = _process.TotalProcessorTime;
        var currentSampleTime = DateTime.UtcNow;
        var elapsedTime = (currentSampleTime - _lastSampleTime).TotalMilliseconds;
        var cpuUsedTime = (currentCpuTime - _lastCpuTime).TotalMilliseconds;
        var cpuUsagePercent = elapsedTime > 0 ? cpuUsedTime / (elapsedTime * _processorCount) * 100.0 : 0;

        _lastCpuTime = currentCpuTime;
        _lastSampleTime = currentSampleTime;

        await _telemetry.TrackMetricAsync("CPU", "Usage %", cpuUsagePercent, _metadata, cancellationToken)
                        .ConfigureAwait(false);
    }
}
