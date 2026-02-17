using Newtonsoft.Json;

namespace Baked.RestApi;

public class NullableJsonConverter<T>(JsonConverter<T> _inner)
    : JsonConverter<T?>() where T : struct
{
    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var result = _inner.ReadJson(reader, objectType, existingValue ?? default, hasExistingValue && existingValue.HasValue, serializer);

        return Equals(result, default(T)) ? null : result;
    }

    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            _inner.WriteJson(writer, value, serializer);
        }
        else
        {
            writer.WriteNull();
        }
    }
}