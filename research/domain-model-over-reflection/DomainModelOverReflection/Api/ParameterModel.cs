using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ParameterModel(string Name, Type Type)
{
    public ParameterModel(ParameterInfo parameterInfo)
        : this(parameterInfo.Name ?? throw new("Parameter should have name"), parameterInfo.ParameterType)
    { }

    public ParameterModel(Models.ParameterModel parameterModel)
        : this(parameterModel.Name, parameterModel.Type)
    { }
}
