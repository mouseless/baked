using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct ParameterModel
{
    public readonly string Name;
    public readonly string Type;

    public ParameterModel(string name, string type)
    {
        Name = name;
        Type = type;
    }

    public ParameterModel(ParameterInfo parameterInfo)
    {
        Name = parameterInfo.Name ?? string.Empty;
        Type = parameterInfo.ParameterType.FullName ?? string.Empty;
    }
}
#pragma warning restore IDE1006 // Naming Styles
