namespace Baked.Domain.Metadata;

public class MetadataSet(string name)
{
    Dictionary<Type, AttributeUsageAttribute?> _usageCache = new();

    public string Name => name;
    public MetadataModelBuilderOptions BuilderOptions { get; } = new();

    public MetadataSet AddAttribute<T>()
    {
        var usage = GetUsage<T>();
        if (usage is null) { return this; }

        if (usage.ValidOn == AttributeTargets.Class)
        {
            BuilderOptions.TypeAttributes.Add(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Method)
        {
            BuilderOptions.MethodAttributes.Add(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Parameter)
        {
            BuilderOptions.ParameterAttributes.Add(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Property)
        {
            BuilderOptions.PropertyAttributes.Add(typeof(T));
        }

        return this;
    }

    public MetadataSet RemoveAttribute<T>()
    {
        var usage = GetUsage<T>();
        if (usage is null) { return this; }

        if (usage.ValidOn == AttributeTargets.Class)
        {
            BuilderOptions.TypeAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Method)
        {
            BuilderOptions.MethodAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Parameter)
        {
            BuilderOptions.ParameterAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Property)
        {
            BuilderOptions.PropertyAttributes.Remove(typeof(T));
        }

        return this;
    }

    AttributeUsageAttribute? GetUsage<T>()
    {
        if (!_usageCache.TryGetValue(typeof(T), out var usage))
        {
            usage = (AttributeUsageAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(AttributeUsageAttribute));
            _usageCache[typeof(T)] = usage;
        }

        return usage;
    }
}