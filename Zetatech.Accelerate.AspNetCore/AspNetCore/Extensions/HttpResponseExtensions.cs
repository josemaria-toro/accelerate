using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.AspNetCore.Extensions;

public static class HttpResponseExtensions
{
    public static Boolean IsBodyInJsonFormat(this HttpResponse httpResponse)
    {
        if (httpResponse == null)
        {
            throw new ArgumentException("The provided http response must be a valid instance", nameof(httpResponse));
        }

        var jsonContentTypes = new String[]
        {
            "application/json",
            "text/json"
        };

        return jsonContentTypes.Any(x => httpResponse.ContentType.Contains(x, StringComparison.InvariantCultureIgnoreCase));
    }
    public static async Task<Byte[]> ReadBodyAsBufferAsync(this HttpResponse httpResponse,
                                                           CancellationToken cancellationToken = default)
    {
        if (httpResponse == null)
        {
            throw new ArgumentException("The provided http response must be a valid instance", nameof(httpResponse));
        }

        var buffer = Array.Empty<Byte>();

        if (httpResponse.Body != null && httpResponse.Body.CanRead)
        {
            if (httpResponse.ContentLength.HasValue)
            {
                buffer = new Byte[httpResponse.ContentLength.Value];
                await httpResponse.Body.ReadAsync(buffer.AsMemory(), cancellationToken)
                                       .ConfigureAwait(false);
            }
            else
            {
                var bytesSize = 65535;
                var bytes = new Byte[bytesSize];
                var bytesReaded = await httpResponse.Body.ReadAsync(buffer.AsMemory(), cancellationToken)
                                                         .ConfigureAwait(false);

                while (bytesReaded >= bytes.Length)
                {
                    Array.Resize(ref bytes, bytes.Length + bytesSize);
                    bytesReaded += await httpResponse.Body.ReadAsync(buffer.AsMemory(bytesReaded));
                }

                Array.Resize(ref bytes, bytesReaded);
            }
        }

        return buffer;
    }
    public static async Task<TBody> ReadBodyAsJsonAsync<TBody>(this HttpResponse httpResponse,
                                                               CancellationToken cancellationToken = default) where TBody : class, new()
    {
        if (httpResponse == null)
        {
            throw new ArgumentException("The provided http response must be a valid instance", nameof(httpResponse));
        }

        TBody jsonObject = default;

        if (httpResponse.IsBodyInJsonFormat())
        {
            var buffer = await httpResponse.ReadBodyAsBufferAsync(cancellationToken)
                                           .ConfigureAwait(false);

            if (buffer.Any())
            {
                jsonObject = Json.ToObject<TBody>(buffer);
            }
        }

        return jsonObject;
    }
    public static async Task<String> ReadBodyAsStringAsync(this HttpResponse httpResponse,
                                                           CancellationToken cancellationToken = default)
    {
        if (httpResponse == null)
        {
            throw new ArgumentException("The provided http response must be a valid instance", nameof(httpResponse));
        }

        var bodyString = String.Empty;
        var buffer = await httpResponse.ReadBodyAsBufferAsync(cancellationToken)
                                       .ConfigureAwait(false);

        if (buffer.Any())
        {
            bodyString = Encoding.UTF8.GetString(buffer);
        }

        return bodyString;
    }
}
