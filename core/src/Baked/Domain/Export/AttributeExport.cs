using Baked.Business;
using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record AttributeExport(string Name)
{
    Func<TypeModelMetadata, string> _typeGroupName = type => type.Name;
    Dictionary<Type, AttributeFilter> _attributeFilters = new();

    public List<AttributeFilter> TypeFilters { get; } = [];
    public List<AttributeFilter> MethodFilters { get; } = [];
    public List<AttributeFilter> ParameterFilters { get; } = [];
    public List<AttributeFilter> PropertyFilters { get; } = [];
    public ITypeExportSerializer Serializer { get; set; } = new KdlTypeExportSerializer();
    public Func<TypeAttributeExportModel, string> ContentGroupName { get; set; } = type => type.GroupName;

    public AttributeFilter Include<T>() where T : Attribute
    {
        if (_attributeFilters.TryGetValue(typeof(T), out var filter))
        {
            return filter;
        }

        _attributeFilters[typeof(T)] = filter = AttributeFilter.From<T>();
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

        return filter;
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

    public record AttributeFilter
    {
        public static AttributeFilter From<T>() where T : Attribute =>
            new(typeof(T));

        public Type Type { get; } = default!;
        public List<Func<MetadataProperty, bool>> PropertyFilters { get; set; } = [];

        public void AddPropertyFilter(List<string> names) =>
            AddPropertyFilter(m => names.Contains(m.Name));

        public void AddPropertyFilter(Func<MetadataProperty, bool> filter) =>
            PropertyFilters.Add(filter);

        AttributeFilter(Type type)
        {
            Type = type;
        }
    }
}