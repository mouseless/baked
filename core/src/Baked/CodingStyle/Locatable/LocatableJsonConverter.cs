using Baked.Business;
using Newtonsoft.Json;

namespace Baked.CodingStyle.Locatable;

public abstract class LocatableJsonConverter<TLocatable, TId>(ILocator<TLocatable> _locator, Func<LocatableInitializations> _getLocatableInitializations)
    : JsonConverter<TLocatable> where TLocatable : class
{
    protected abstract string IdProp { get; }
    protected abstract TId GetId(TLocatable entity);
    protected abstract IEnumerable<string> LabelProps { get; }
    protected abstract TId GetLabel(TLocatable entity, string labelProp);

    public override TLocatable? ReadJson(JsonReader reader, Type objectType, TLocatable? existingValue, bool hasExistingValue, JsonSerializer serializer)
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

        var (result, initialize) = _locator.LocateLazily(id.Value);

        _getLocatableInitializations().Add(initialize);

        return result;
    }

    public override void WriteJson(JsonWriter writer, TLocatable? value, JsonSerializer serializer)
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