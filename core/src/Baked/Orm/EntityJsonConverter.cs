using Newtonsoft.Json;

namespace Baked.Orm;

public abstract class EntityJsonConverter<TEntity, TId>(IQueryContext<TEntity> _queryContext)
    : JsonConverter<TEntity> where TEntity : class
{
    protected abstract string IdProp { get; }
    protected abstract TId GetId(TEntity entity);
    protected abstract IEnumerable<string> LabelProps { get; }
    protected abstract TId GetLabel(TEntity entity, string labelProp);

    public override TEntity? ReadJson(JsonReader reader, Type objectType, TEntity? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) { return null; }
        if (reader.TokenType != JsonToken.StartObject) { throw new JsonSerializationException($"Expected object, got {reader.TokenType}"); }

        Business.Id? id = null;
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.EndObject) { break; }
            if (reader.TokenType != JsonToken.PropertyName) { continue; }

            var propName = (string?)reader.Value;
            if (!IdProp.Equals(propName)) { continue; }

            if (!reader.Read()) { break; }
            if (reader.Value is null) { continue; }

            id = Business.Id.Parse(reader.Value);
        }

        if (!id.HasValue) { return null; }

        return _queryContext.SingleById(id.Value);
    }

    public override void WriteJson(JsonWriter writer, TEntity? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName(IdProp);
        writer.WriteValue(GetId(value));

        foreach (var labelProp in LabelProps)
        {
            writer.WritePropertyName(labelProp);
            writer.WriteValue(GetLabel(value, labelProp));
        }

        writer.WriteEndObject();
    }
}