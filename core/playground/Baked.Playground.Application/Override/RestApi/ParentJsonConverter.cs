using Baked.Business;
using Baked.Orm;
using Baked.Playground.Orm;
using Humanizer;
using Newtonsoft.Json;

namespace Baked.Playground.Override.RestApi;

public class ParentJsonConverter(IServiceProvider _sp)
    : JsonConverter<Parent>
{
    public override Parent ReadJson(JsonReader reader, Type objectType, Parent? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) { return null!; }
        if (reader.TokenType != JsonToken.StartObject) { throw new JsonSerializationException($"Expected object, got {reader.TokenType}"); }

        Id? id = default;
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var name = (string)reader.Value!;
                reader.Read();

                if (string.Equals(name, "id", StringComparison.OrdinalIgnoreCase))
                {
                    id = Id.Parse(reader.Value ?? "Whatt!!");
                }
                else
                {
                    reader.Skip();
                }
            }
            else if (reader.TokenType == JsonToken.EndObject)
            {
                break;
            }
        }

        if (!id.HasValue) { throw new("id is required"); }

        return _sp
            .UsingCurrentScope()
            .GetRequiredService<IQueryContext<Parent>>()
            .SingleById(id.Value);
    }

    public override void WriteJson(JsonWriter writer, Parent? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName(nameof(Parent.Id).Camelize());
        writer.WriteValue($"{value.Id}");
        writer.WritePropertyName(nameof(Parent.Name).Camelize());
        writer.WriteValue($"{value.Name}");
        writer.WriteEndObject();
    }
}