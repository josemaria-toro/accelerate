using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zetatech.Accelerate.Serialization;

namespace Zetatech.Accelerate.Extensions;

public static class HttpRequestExtensions
{
    public static Boolean IsBodyInJsonFormat(this HttpRequest httpRequest)
    {
        if (httpRequest == null)
        {
            throw new ArgumentException("The provided http request must be a valid instance", nameof(httpRequest));
        }

        var jsonContentTypes = new String[]
        {
            "application/json",
            "text/json"
        };

        return jsonContentTypes.Any(x => httpRequest.ContentType.Contains(x, StringComparison.InvariantCultureIgnoreCase));
    }
    public static async Task<Byte[]> ReadBodyAsBufferAsync(this HttpRequest httpRequest)
    {
        if (httpRequest == null)
        {
            throw new ArgumentException("The provided http request must be a valid instance", nameof(httpRequest));
        }

        var buffer = Array.Empty<Byte>();

        if (httpRequest.Body != null && httpRequest.Body.CanRead)
        {
            if (httpRequest.ContentLength.HasValue)
            {
                buffer = new Byte[httpRequest.ContentLength.Value];
                await httpRequest.Body.ReadAsync(buffer.AsMemory());
            }
            else
            {
                var bytesSize = 65535;
                var bytes = new Byte[bytesSize];
                var bytesReaded = await httpRequest.Body.ReadAsync(buffer.AsMemory());

                while (bytesReaded >= bytes.Length)
                {
                    Array.Resize(ref bytes, bytes.Length + bytesSize);
                    bytesReaded += await httpRequest.Body.ReadAsync(buffer.AsMemory(bytesReaded));
                }

                Array.Resize(ref bytes, bytesReaded);
            }
        }

        return buffer;
    }
    public static async Task<TBody> ReadBodyAsJsonAsync<TBody>(this HttpRequest httpRequest) where TBody : class, new()
    {
        if (httpRequest == null)
        {
            throw new ArgumentException("The provided http request must be a valid instance", nameof(httpRequest));
        }

        TBody jsonObject = default;

        if (httpRequest.IsBodyInJsonFormat())
        {
            var buffer = await httpRequest.ReadBodyAsBufferAsync();

            if (buffer.Any())
            {
                jsonObject = Json.ToObject<TBody>(buffer);
            }
        }

        return jsonObject;
    }
    public static async Task<String> ReadBodyAsStringAsync(this HttpRequest httpRequest)
    {
        if (httpRequest == null)
        {
            throw new ArgumentException("The provided http request must be a valid instance", nameof(httpRequest));
        }

        var bodyString = String.Empty;
        var buffer = await httpRequest.ReadBodyAsBufferAsync();

        if (buffer.Any())
        {
            bodyString = Encoding.UTF8.GetString(buffer);
        }

        return bodyString;
    }
}
