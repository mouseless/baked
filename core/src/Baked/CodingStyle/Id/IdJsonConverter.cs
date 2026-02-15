using Newtonsoft.Json;

namespace Baked.CodingStyle.Id;

public class IdJsonConverter : JsonConverter<Business.Id>
{
    public override Business.Id ReadJson(JsonReader reader, Type objectType, Business.Id existingValue, bool hasExistingValue, JsonSerializer serializer) =>
        Business.Id.Create(reader.Value ?? string.Empty);

    public override void WriteJson(JsonWriter writer, Business.Id value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString());
}