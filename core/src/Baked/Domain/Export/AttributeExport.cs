using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record AttributeExport(string Name)
{
    Func<TypeModelMetadata, string> _typeGroupName = type => type.Name;

    public List<Type> TypeAttributes { get; set; } = [];
    public List<Type> MethodAttributes { get; set; } = [];
    public List<Type> ParameterAttributes { get; set; } = [];
    public List<Type> PropertyAttributes { get; set; } = [];

    public void Include<T>(
        bool requireUsage = true
    ) where T : Attribute
    {
        var usage = GetUsage<T>();
        if (usage is null) { return; }

        if (!requireUsage || usage.ValidOn == AttributeTargets.Class)
        {
            TypeAttributes.Add(typeof(T));
        }

        if (!requireUsage || usage.ValidOn == AttributeTargets.Method)
        {
            MethodAttributes.Add(typeof(T));
        }

        if (!requireUsage || usage.ValidOn == AttributeTargets.Parameter)
        {
            ParameterAttributes.Add(typeof(T));
        }

        if (!requireUsage || usage.ValidOn == AttributeTargets.Property)
        {
            PropertyAttributes.Add(typeof(T));
        }
    }

    public void Exclude<T>() where T : Attribute
    {
        var usage = GetUsage<T>();
        if (usage is null) { return; }

        if (usage.ValidOn == AttributeTargets.Class)
        {
            TypeAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Method)
        {
            MethodAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Parameter)
        {
            ParameterAttributes.Remove(typeof(T));
        }

        if (usage.ValidOn == AttributeTargets.Property)
        {
            PropertyAttributes.Remove(typeof(T));
        }
    }

    public void TypeGroupName(Func<TypeModelMetadata, string> typeGroupName) =>
        _typeGroupName = typeGroupName;

    internal string GetTypeGroupName(TypeModelMetadata type) =>
        _typeGroupName(type);

    AttributeUsageAttribute? GetUsage<T>() =>
        (AttributeUsageAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(AttributeUsageAttribute));
}
