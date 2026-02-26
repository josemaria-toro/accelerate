using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Application;
using Zetatech.Accelerate.Extensions;

namespace Zetatech.Accelerate.Infrastructure;

public sealed class Tracker : ITracker
{
    private Boolean _disposed;
    private HttpClient _httpClient;
    private TrackerOptions _options;
    private JsonSerializerOptions _serializerOptions;

    public Tracker(IOptions<TrackerOptions> options)
    {
        _options = options.Value;
        _httpClient = new();
        _serializerOptions = JsonSerializerOptions.Default.GetDefaultOptions();

        var sections = _options.ConnectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var parameters in sections)
        {
            var parameter = parameters.Split("=", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (parameter[0].Equals("ingestionKey", StringComparison.InvariantCultureIgnoreCase))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", parameter[1]);
            }
            else if (parameter[0].Equals("ingestionUrl", StringComparison.InvariantCultureIgnoreCase))
            {
                if (parameter[1].EndsWith('/'))
                {
                    _httpClient.BaseAddress = new Uri(parameter[1]);
                }
                else
                {
                    _httpClient.BaseAddress = new Uri($"{parameter[1]}/");
                }
            }
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
        _httpClient = null;
        _options = null;
        _serializerOptions = null;

        GC.SuppressFinalize(this);
    }
    public void Track(Dependency dependency) => TrackObject(dependency, "dependencies");
    public void Track(Error error) => TrackObject(error, "errors");
    public void Track(Event @event) => TrackObject(@event, "events");
    public void Track(Metric metric) => TrackObject(metric, "metrics");
    public void Track(PageView pageView) => TrackObject(pageView, "pageviews");
    public void Track(Request request) => TrackObject(request, "requests");
    public void Track(Test test) => TrackObject(test, "tests");
    public void Track(Trace trace) => TrackObject(trace, "traces");
    private void TrackObject(Object @object, String urlPath)
    {
        var jsonString = JsonSerializer.Serialize(@object, _serializerOptions);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        _ = _httpClient.PostAsync(urlPath, stringContent, CancellationToken.None);
    }
}