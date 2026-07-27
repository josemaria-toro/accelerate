using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zetatech.Accelerate.Http.Clients;

public static class JsonClientAsync
{
    public static async Task<HttpResponseMessage> DeleteAsync<TBody>(this JsonClient jsonClient,
                                                                     Uri uri,
                                                                     TBody body = null,
                                                                     IDictionary<String, String> headers = null,
                                                                     CancellationToken cancellationToken = default) where TBody : class
    {
        return await Task.Run(() => jsonClient.Delete(uri, body, headers), cancellationToken);
    }
    public static async Task<HttpResponseMessage> GetAsync(this JsonClient jsonClient,
                                                           Uri uri,
                                                           IDictionary<String, String> headers = null,
                                                           CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => jsonClient.Get(uri, headers), cancellationToken);
    }
    public static async Task<HttpResponseMessage> PatchAsync<TBody>(this JsonClient jsonClient,
                                                                    Uri uri,
                                                                    TBody body = null,
                                                                    IDictionary<String, String> headers = null,
                                                                    CancellationToken cancellationToken = default) where TBody : class
    {
        return await Task.Run(() => jsonClient.Patch(uri, body, headers), cancellationToken);
    }
    public static async Task<HttpResponseMessage> PostAsync<TBody>(this JsonClient jsonClient,
                                                                   Uri uri,
                                                                   TBody body = null,
                                                                   IDictionary<String, String> headers = null,
                                                                   CancellationToken cancellationToken = default) where TBody : class
    {
        return await Task.Run(() => jsonClient.Post(uri, body, headers), cancellationToken);
    }
    public static async Task<HttpResponseMessage> PutAsync<TBody>(this JsonClient jsonClient,
                                                                  Uri uri,
                                                                  TBody body = null,
                                                                  IDictionary<String, String> headers = null,
                                                                  CancellationToken cancellationToken = default) where TBody : class
    {
        return await Task.Run(() => jsonClient.Put(uri, body, headers), cancellationToken);
    }
}
