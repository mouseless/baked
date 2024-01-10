using System.Reflection;

namespace Do.Domain.Model;

public record ParameterModel(string Name, Type Type)
{
    public ParameterModel(ParameterInfo parameterInfo)
        : this(parameterInfo.Name ?? string.Empty, parameterInfo.ParameterType)
    { }
}
