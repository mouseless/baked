using Baked.Business;

namespace Baked.Domain.Export;

public class AttributeDatas : IAttributeDataBuilder
{
    Dictionary<Type, Func<Attribute, List<AttributeProperty>>> _dataBuilders = new();

    public void Set<T>(Func<T, List<AttributeProperty>> data) where T : Attribute
    {
        _dataBuilders[typeof(T)] = attr => data((T)attr);
    }

    List<AttributeProperty> IAttributeDataBuilder.Build(object instance) =>
        _dataBuilders.TryGetValue(instance.GetType(), out var builder) ? builder.Invoke((Attribute)instance) : [];
}