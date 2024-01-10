using System.Reflection;

namespace Do.Domain.Model;

public record ParameterModel(string Name)
{
    public ParameterModel(ParameterInfo parameterInfo)
        : this(parameterInfo.Name ?? string.Empty)
    { }
}
