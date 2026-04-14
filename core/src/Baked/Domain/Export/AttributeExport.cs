using Baked.Domain.Model;

namespace Baked.Domain.Export;

public partial record AttributeExport(string Name)
{
    Func<TypeModelMetadata, string> _typeGroupName = type => type.Name;
    Dictionary<Type, IAttributeFilter> _attributeFilters = new();

    public List<IAttributeFilter> TypeFilters { get; } = [];
    public List<IAttributeFilter> MethodFilters { get; } = [];
    public List<IAttributeFilter> ParameterFilters { get; } = [];
    public List<IAttributeFilter> PropertyFilters { get; } = [];
    public ITypeExportSerializer Serializer { get; set; } = new KdlTypeExportSerializer();
    public Func<TypeAttributeExportModel, string> ContentGroupName { get; set; } = type => type.GroupName;

    public AttributeFilter<T> Include<T>() where T : Attribute
    {
        if (_attributeFilters.TryGetValue(typeof(T), out var filter))
        {
            return (AttributeFilter<T>)filter;
        }

        _attributeFilters[typeof(T)] = filter = new AttributeFilter<T>();
        var usage = GetUsage<T>();
        if (
            usage is null ||
            usage.ValidOn.HasFlag(AttributeTargets.Class) ||
            usage.ValidOn.HasFlag(AttributeTargets.Interface) ||
            usage.ValidOn.HasFlag(AttributeTargets.Struct) ||
            usage.ValidOn.HasFlag(AttributeTargets.Enum)
        )
        {
            TypeFilters.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Method))
        {
            MethodFilters.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Parameter))
        {
            ParameterFilters.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Property))
        {
            PropertyFilters.Add(filter);
        }

        return (AttributeFilter<T>)filter;
    }

    public void Exclude<T>() where T : Attribute
    {
        if (!_attributeFilters.TryGetValue(typeof(T), out var filter))
        {
            return;
        }

        _attributeFilters.Remove(typeof(T));
        var usage = GetUsage<T>();

        if (
            usage is null ||
            usage.ValidOn.HasFlag(AttributeTargets.Class) ||
            usage.ValidOn.HasFlag(AttributeTargets.Interface) ||
            usage.ValidOn.HasFlag(AttributeTargets.Struct) ||
            usage.ValidOn.HasFlag(AttributeTargets.Enum)
        )
        {
            TypeFilters.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Method))
        {
            MethodFilters.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Parameter))
        {
            ParameterFilters.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Property))
        {
            PropertyFilters.Remove(filter);
        }
    }

    public void TypeGroupName(Func<TypeModelMetadata, string> typeGroupName) =>
        _typeGroupName = typeGroupName;

    internal string GetTypeGroupName(TypeModelMetadata type) =>
        _typeGroupName(type);

    AttributeUsageAttribute? GetUsage<T>() =>
        (AttributeUsageAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(AttributeUsageAttribute));
}