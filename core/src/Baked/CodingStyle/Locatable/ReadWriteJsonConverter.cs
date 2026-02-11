using Baked.Business;
using Newtonsoft.Json;

namespace Baked.CodingStyle.Locatable;

public abstract class ReadWriteJsonConverter<TLocatable>(ILocator<TLocatable> _locator, Func<LocatableInitializations> _getLocatableInitializations)
    : ReadOnlyJsonConverter<TLocatable>(_locator, _getLocatableInitializations) where TLocatable : class
{
    public override bool CanWrite => true;

    protected abstract string GetId(TLocatable entity);
    protected abstract IEnumerable<string> LabelProps { get; }
    protected abstract string GetLabel(TLocatable entity, string labelProp);

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