using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

public struct ParameterModel
{
    public string Name { get; set; }
    public string Type { get; set; }

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
