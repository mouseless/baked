using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ParameterModel(string Name, string Type)
{
    public ParameterModel(ParameterInfo parameterInfo)
        : this(parameterInfo.Name ?? throw new("Parameter should have name"), parameterInfo.ParameterType.FullName ?? throw new("ParameterType should have name"))
    { }

    public ParameterModel(Models.Domain.ParameterModel parameterModel)
        : this(parameterModel.Name, parameterModel.Type)
    { }
}
