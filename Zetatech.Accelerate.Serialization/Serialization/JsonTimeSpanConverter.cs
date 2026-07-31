using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zetatech.Accelerate.Serialization;

internal sealed class JsonTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var milliseconds = reader.GetDouble();
        return TimeSpan.FromMilliseconds(milliseconds);
    }
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.TotalMilliseconds);
    }
}
