using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Serialization;

internal sealed class JsonIPAddressConverter : JsonConverter<IPAddress>
{
    public override IPAddress Read(ref Utf8JsonReader reader,
                                   Type typeToConvert,
                                   JsonSerializerOptions options)
    {
        var ipAddressString = reader.GetString();
        return IPAddress.Parse(ipAddressString);
    }
    public override void Write(Utf8JsonWriter writer,
                               IPAddress value,
                               JsonSerializerOptions options)
    {
        var ipAddressString = value.ToString();
        writer.WriteStringValue(ipAddressString);
    }
}
