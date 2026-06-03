using Baked.Business;
using Baked.RestApi;
using Newtonsoft.Json;

namespace Baked.CodingStyle.Locatable;

public abstract class ReadWriteJsonConverter<TLocatable>(ILocator<TLocatable> _locator, Func<LocatableInitializations> _getLocatableInitializations)
    : ReadOnlyJsonConverter<TLocatable>(_locator, _getLocatableInitializations) where TLocatable : class
{
    public override bool CanWrite => true;

    protected abstract string GetId(TLocatable entity);
    protected abstract IEnumerable<string> LabelProps { get; }
    protected abstract void WriteLabel(JsonWriter writer, TLocatable entity, JsonSerializer serializer, string labelProp);

    public override void WriteJson(JsonWriter writer, TLocatable? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        var contractResolver = serializer.ContractResolver as ExtendedContractResolver;
        writer.WriteStartObject();

        if (serializer.TypeNameHandling == TypeNameHandling.All ||
            serializer.TypeNameHandling == TypeNameHandling.Auto && typeof(TLocatable).IsInterface ||
            serializer.TypeNameHandling == TypeNameHandling.Objects
        )
        {
            var actualType = contractResolver is not null
                ? contractResolver.ClearProxyType(value.GetType())
                : value.GetType();
            writer.WritePropertyName("$type");

            serializer.SerializationBinder.BindToName(actualType, out var assemblyName, out var typeName);
            if (assemblyName is not null) { typeName += $", {assemblyName}"; }

            writer.WriteValue(typeName);
        }

        writer.WritePropertyName(IdProp);
        writer.WriteValue(GetId(value));

        if (contractResolver is not null && contractResolver.IsUnitializedProxy(value))
        {
            writer.WriteEndObject();

            return;
        }

        foreach (var labelProp in LabelProps)
        {
            writer.WritePropertyName(labelProp);
            WriteLabel(writer, value, serializer, labelProp);
        }

        writer.WriteEndObject();
    }
}