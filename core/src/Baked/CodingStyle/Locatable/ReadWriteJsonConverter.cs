using Baked.Business;
using Newtonsoft.Json;
using NHibernate.Proxy;

namespace Baked.CodingStyle.Locatable;

public abstract class ReadWriteJsonConverter<TLocatable>(ILocator<TLocatable> _locator, Func<LocatableInitializations> _getLocatableInitializations)
    : ReadOnlyJsonConverter<TLocatable>(_locator, _getLocatableInitializations) where TLocatable : class
{
    public override bool CanWrite => true;

    protected abstract string GetId(TLocatable entity);
    protected abstract IEnumerable<string> LabelProps { get; }
    protected abstract Action<JsonWriter, JsonSerializer> GetLabelWriter(TLocatable entity, string labelProp);

    public override void WriteJson(JsonWriter writer, TLocatable? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        writer.WriteStartObject();

        if (serializer.TypeNameHandling == TypeNameHandling.All ||
            serializer.TypeNameHandling == TypeNameHandling.Auto && typeof(TLocatable).IsInterface ||
            serializer.TypeNameHandling == TypeNameHandling.Objects
        )
        {
            var actualType = value.GetType();
            writer.WritePropertyName("$type");

            serializer.SerializationBinder.BindToName(actualType, out var assemblyName, out var typeName);
            if (assemblyName is not null) { typeName += $", {assemblyName}"; }

            writer.WriteValue(typeName);
        }

        writer.WritePropertyName(IdProp);
        writer.WriteValue(GetId(value));

        if (!value.GetType().IsAssignableTo(typeof(INHibernateProxy)))
        {
            foreach (var labelProp in LabelProps)
            {
                writer.WritePropertyName(labelProp);
                var labelWriter = GetLabelWriter(value, labelProp);
                labelWriter(writer, serializer);
            }
        }

        writer.WriteEndObject();
    }
}