namespace Baked.Domain.Export;

public class AttributeProperties : IAttributePropertyBuilder
{
    Dictionary<Type, Func<Attribute, List<AttributeProperty>>> _dataBuilders = new();

    public void Set<T>(Func<T, List<AttributeProperty>> data) where T : Attribute
    {
        _dataBuilders[typeof(T)] = attr => data((T)attr);
    }

    List<AttributeProperty> IAttributePropertyBuilder.Build(object instance) =>
        _dataBuilders.TryGetValue(instance.GetType(), out var builder) ? builder.Invoke((Attribute)instance) : [];
}