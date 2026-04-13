using Humanizer;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Baked.Binding.Rest;

public class PolymorphicSerializationBinder(
    ISerializationBinder? _fallback = default
) : ISerializationBinder
{
    readonly ISerializationBinder _fallback = _fallback ?? new DefaultSerializationBinder();

    public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
    {
        var suffix = serializedType.GetInterfaces()
            .Select(i => i.Name.TrimStart('I'))
            .FirstOrDefault(suffix => serializedType.Name.EndsWith(suffix));
        if (suffix is null)
        {
            _fallback.BindToName(serializedType, out assemblyName, out typeName);

            return;
        }

        assemblyName = null;
        typeName = CultureInfo.UsingInvariantCulture(() =>
            serializedType.Name[..^suffix.Length].Camelize()
        );
    }

    public Type BindToType(string? assemblyName, string typeName) =>
        _fallback.BindToType(assemblyName, typeName);
}