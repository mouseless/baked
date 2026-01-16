using Newtonsoft.Json;

namespace Baked.Orm.AutoMap;

public class IdJsonConverter : JsonConverter<Id>
{
    public override Id ReadJson(JsonReader reader, Type objectType, Id existingValue, bool hasExistingValue, JsonSerializer serializer) =>
        Id.Parse(reader.Value ?? string.Empty);

    public override void WriteJson(JsonWriter writer, Id value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString());
}