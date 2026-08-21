using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Serialization;

internal sealed class JsonByteArrayConverter : JsonConverter<Byte[]>
{
    public override Byte[] Read(ref Utf8JsonReader reader,
                                Type typeToConvert,
                                JsonSerializerOptions options)
    {
        var base64String = reader.GetString();
        return Convert.FromBase64String(base64String);
    }
    public override void Write(Utf8JsonWriter writer,
                               Byte[] value,
                               JsonSerializerOptions options)
    {
        var base64String = Convert.ToBase64String(value);
        writer.WriteStringValue(base64String);
    }
}
