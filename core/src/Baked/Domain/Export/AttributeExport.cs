using Baked.Business;
using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record AttributeExport(string Name)
{
    Func<TypeModelMetadata, string> _typeGroupName = type => type.Name;

    public List<AttributeFilter> TypeFilter { get; set; } = [];
    public List<AttributeFilter> MethodFilters { get; set; } = [];
    public List<AttributeFilter> ParameterFilters { get; set; } = [];
    public List<AttributeFilter> PropertyFilters { get; set; } = [];

    public void Include<T>(
        Func<MetadataProperty, bool>? propertyFilter = default
    ) where T : Attribute
    {
        var usage = GetUsage<T>();
        var filter = AttributeFilter.From<T>(propertyFilter);

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Class))
        {
            TypeFilter.Add(filter);
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
    }

    public void Exclude<T>() where T : Attribute
    {
        var usage = GetUsage<T>();

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Class))
        {
            var item = TypeFilter.FirstOrDefault(f => f.Type == typeof(T));
            if (item is null) { return; }

            TypeFilter.Remove(item);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Method))
        {
            var item = MethodFilters.FirstOrDefault(f => f.Type == typeof(T));
            if (item is null) { return; }

            MethodFilters.Remove(item);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Parameter))
        {
            var item = ParameterFilters.FirstOrDefault(f => f.Type == typeof(T));
            if (item is null) { return; }

            ParameterFilters.Remove(item);
        }

        if (usage is null || usage.ValidOn.HasFlag(AttributeTargets.Property))
        {
            var item = PropertyFilters.FirstOrDefault(f => f.Type == typeof(T));
            if (item is null) { return; }

            PropertyFilters.Remove(item);
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
        public static AttributeFilter From<T>(
            Func<MetadataProperty, bool>? propertyFilter = default
        ) where T : Attribute =>
            new(typeof(T)) { PropertyFilter = propertyFilter };

        public Type Type { get; }
        public Func<MetadataProperty, bool>? PropertyFilter { get; set; }

        AttributeFilter(Type type)
        {
            Type = type;
        }
    }
}