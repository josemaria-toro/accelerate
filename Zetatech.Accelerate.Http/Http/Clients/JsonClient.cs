using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.Http.Clients;

public sealed class JsonClient : HttpClient
{
    private Boolean _disposed;
    private readonly JsonClientOptions _options;

    public JsonClient(IOptions<JsonClientOptions> options) : base(new JsonClientHandler(options?.Value))
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));

        if (_options.BasicCredentials != null)
        {
            var credentials = $"{_options.BasicCredentials.UserName}:{_options.BasicCredentials.Password}";
            var buffer = Encoding.Default.GetBytes(credentials);
            var base64Credentials = Convert.ToBase64String(buffer);

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        }
        else if (!String.IsNullOrEmpty(_options.BearerToken))
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.BearerToken);
        }
    }

    protected override void Dispose(Boolean disposing)
    {
        base.Dispose(disposing);

        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }

        _disposed = true;
    }
    private HttpRequestMessage BuildHttpRequestMessage(Uri uri, HttpMethod httpMethod, Object body = null, IDictionary<String, String> headers = null)
    {
        var httpRequestMessage = new HttpRequestMessage(httpMethod, uri);

        if (body != null)
        {
            var jsonBody = Json.ToString(body);
            var mediaTypeHeader = MediaTypeHeaderValue.Parse(MediaTypeNames.Application.Json);

            httpRequestMessage.Content = new StringContent(jsonBody, _options.Encoding, mediaTypeHeader);
        }

        if (headers != null)
        {
            foreach (var header in headers)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        var activity = Activity.Current;

        if (activity != null)
        {
            var traceParent = $"00-{activity.TraceId}-{activity.SpanId}-{(activity.Recorded ? "01" : "00")}";

            httpRequestMessage.Headers.Add("traceparent", traceParent);

            if (!String.IsNullOrEmpty(activity.TraceStateString))
            {
                httpRequestMessage.Headers.Add("tracestate", Activity.Current.TraceStateString);
            }
        }

        return httpRequestMessage;
    }
    public async Task<HttpResponseMessage> DeleteAsync<TBody>(Uri uri, TBody body = null, IDictionary<String, String> headers = null, CancellationToken cancellationToken = default) where TBody : class
    {
        var httpRequestMessage = BuildHttpRequestMessage(uri, HttpMethod.Delete, body, headers);

        return await base.SendAsync(httpRequestMessage, cancellationToken)
                         .ConfigureAwait(false);
    }
    public async Task<HttpResponseMessage> GetAsync(Uri uri, IDictionary<String, String> headers = null, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = BuildHttpRequestMessage(uri, HttpMethod.Get, null, headers);

        return await base.SendAsync(httpRequestMessage, cancellationToken)
                         .ConfigureAwait(false);
    }
    public async Task<HttpResponseMessage> PatchAsync<TBody>(Uri uri, TBody body = null, IDictionary<String, String> headers = null, CancellationToken cancellationToken = default) where TBody : class
    {
        var httpRequestMessage = BuildHttpRequestMessage(uri, HttpMethod.Patch, body, headers);

        return await base.SendAsync(httpRequestMessage, cancellationToken)
                         .ConfigureAwait(false);
    }
    public async Task<HttpResponseMessage> PostAsync<TBody>(Uri uri, TBody body = null, IDictionary<String, String> headers = null, CancellationToken cancellationToken = default) where TBody : class
    {
        var httpRequestMessage = BuildHttpRequestMessage(uri, HttpMethod.Post, body, headers);

        return await base.SendAsync(httpRequestMessage, cancellationToken)
                         .ConfigureAwait(false);
    }
    public async Task<HttpResponseMessage> PutAsync<TBody>(Uri uri, TBody body = null, IDictionary<String, String> headers = null, CancellationToken cancellationToken = default) where TBody : class
    {
        var httpRequestMessage = BuildHttpRequestMessage(uri, HttpMethod.Put, body, headers);

        return await base.SendAsync(httpRequestMessage, cancellationToken)
                         .ConfigureAwait(false);
    }
}
