namespace Baked.Domain.Export;

public class AttributeProperties : IAttributePropertyBuilder
{
    Dictionary<Type, Func<Attribute, List<AttributeProperty>>> _builders = new();

    public void Set<T>(Func<T, List<AttributeProperty>> data) where T : Attribute
    {
        _builders[typeof(T)] = attr => data((T)attr);
    }

    List<AttributeProperty> IAttributePropertyBuilder.Build(object instance) =>
        _builders.TryGetValue(instance.GetType(), out var builder) ? builder.Invoke((Attribute)instance) : [];
}