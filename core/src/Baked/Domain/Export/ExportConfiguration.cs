using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record ExportConfiguration(string Name)
{
    Func<TypeModelMetadata, string> _typeGroupName = type => type.Name;
    Dictionary<Type, IAttributeExport> _attributeFilters = new();

    public List<IAttributeExport> Type { get; } = [];
    public List<IAttributeExport> Property { get; } = [];
    public List<IAttributeExport> Method { get; } = [];
    public List<IAttributeExport> Parameter { get; } = [];
    public ITypeExportSerializer Serializer { get; set; } = new KdlTypeExportSerializer();
    public Func<TypeExportModel, string> ContentGroupName { get; set; } = type => type.GroupName;

    public AttributeExport<T> Include<T>() where T : Attribute
    {
        if (_attributeFilters.TryGetValue(typeof(T), out var filter))
        {
            return (AttributeExport<T>)filter;
        }

        _attributeFilters[typeof(T)] = filter = new AttributeExport<T>();
        var usage = GetUsage<T>();
        if (
            usage is null ||
            usage.ValidOn.HasFlag(AttributeTargets.Class) ||
            usage.ValidOn.HasFlag(AttributeTargets.Interface) ||
            usage.ValidOn.HasFlag(AttributeTargets.Struct) ||
            usage.ValidOn.HasFlag(AttributeTargets.Enum)
        )
        {
            Type.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Property))
        {
            Property.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Method))
        {
            Method.Add(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Parameter))
        {
            Parameter.Add(filter);
        }

        return (AttributeExport<T>)filter;
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
            Type.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Property))
        {
            Property.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Method))
        {
            Method.Remove(filter);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Parameter))
        {
            Parameter.Remove(filter);
        }
    }

    public void TypeGroupName(Func<TypeModelMetadata, string> typeGroupName) =>
        _typeGroupName = typeGroupName;

    internal string GetTypeGroupName(TypeModelMetadata type) =>
        _typeGroupName(type);

    AttributeUsageAttribute? GetUsage<T>() =>
        (AttributeUsageAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(AttributeUsageAttribute));
}