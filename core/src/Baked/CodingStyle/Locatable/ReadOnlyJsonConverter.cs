using Baked.Business;
using Newtonsoft.Json;

namespace Baked.CodingStyle.Locatable;

public abstract class ReadOnlyJsonConverter<TLocatable>(ILocator<TLocatable> _locator, Func<LocatableInitializations> _getLocatableInitializations)
    : JsonConverter<TLocatable>() where TLocatable : class
{
    protected abstract string IdProp { get; }
    public override bool CanWrite => false;

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

            id = Business.Id.Create(reader.Value);
        }

        if (!id.HasValue) { return null; }

        var (result, initialize) = _locator.LocateLazily(id.Value);
        _getLocatableInitializations().Add(initialize);

        return result;
    }

    public override void WriteJson(JsonWriter writer, TLocatable? value, JsonSerializer serializer) =>
        throw new NotImplementedException();
}