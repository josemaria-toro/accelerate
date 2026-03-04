using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Zetatech.Accelerate.Extensions;

public static class HttpRequestExtensions
{
    public static Boolean IsJsonContentType(this HttpRequest httpRequest)
    {
        var jsonContentTypes = new String[]
        {
            "application/json",
            "text/json"
        };

        return jsonContentTypes.Any(x => httpRequest.ContentType.Contains(x, StringComparison.InvariantCultureIgnoreCase));
    }
    public static TBody ReadAsJson<TBody>(this HttpRequest httpRequest) where TBody : class, new()
    {
        var bytes = new List<Byte>();
        var continueReading = true;
        var serializerOptions = JsonSerializerOptions.Default.GetDefaultOptions();

        while (continueReading)
        {
            var buffer = new Byte[1024];
            var readTask = httpRequest.Body.ReadAsync(buffer, 0, buffer.Length);

            if (readTask.IsCompletedSuccessfully)
            {
                bytes.AddRange(buffer[..readTask.Result]);

                if (readTask.Result < buffer.Length)
                {
                    continueReading = false;
                }
            }
            else
            {
                continueReading = false;
            }
        }

        return bytes.Any() ? JsonSerializer.Deserialize<TBody>(bytes.ToArray(), serializerOptions) : default(TBody);
    }
}
