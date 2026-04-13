using Baked.Business;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Export;

public class AttributeDataBuilderCollection
{
    Dictionary<Type, Func<Attribute, List<AttributeProperty>>> _dataBuilders = new();

    public void Create<T>(Func<T, List<AttributeProperty>> data) where T : Attribute
    {
        _dataBuilders[typeof(T)] = attr => data((T)attr);
    }

    public bool TryGet<T>([NotNullWhen(true)] out Func<Attribute, List<AttributeProperty>>? builder)
    {
        builder = _dataBuilders.TryGetValue(typeof(T), out builder) ? builder : default;

        return builder != null;
    }
}