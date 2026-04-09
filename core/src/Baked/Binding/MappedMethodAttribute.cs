using Baked.Business;

namespace Baked.Binding;

[AttributeUsage(AttributeTargets.Method)]
public class MappedMethodAttribute(string typeFullName, string methodName)
    : Attribute(), IMetadataSerializer
{
    public string TypeFullName { get; } = typeFullName;
    public string MethodName { get; } = methodName;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(TypeFullName),
            new(MethodName),
        ];
}