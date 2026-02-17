using Newtonsoft.Json;
using System.Globalization;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeJsonConverter<T> : JsonConverter<T>
    where T : struct, IParsable<T>
{
    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) { return default; }
        if (reader.TokenType != JsonToken.String)
        {
            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing {typeof(T)}");
        }

        var value = (string?)reader.Value;
        if (value is null) { return default; }

        return T.Parse(value, CultureInfo.InvariantCulture);
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString());
}