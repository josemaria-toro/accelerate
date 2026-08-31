using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace Zetatech.Accelerate.Http.Clients;

internal sealed class JsonClientHandler : HttpClientHandler
{
    private Boolean _disposed;
    private readonly JsonClientOptions _options;

    public JsonClientHandler(IOptions<JsonClientOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("The provided configuration options must be a valid instance", nameof(options));

        if (_options.AutoRedirect)
        {
            AllowAutoRedirect = _options.AutoRedirect;
            MaxAutomaticRedirections = _options.MaxRedirections;
        }

        if (_options.ClientCertificate != null)
        {
            ClientCertificates.Add(_options.ClientCertificate);
        }

        if (_options.UseProxy)
        {
            Proxy = new WebProxy
            {
                Address = _options.ProxyUri,
                Credentials = _options.ProxyCredentials
            };
            UseProxy = _options.UseProxy;
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
}
