using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public record ParameterModel(string Name, Type Type)
{
    public ParameterModel(ParameterInfo parameterInfo)
        : this(parameterInfo.Name ?? throw new("Parameter should have name"), parameterInfo.ParameterType)
    { }

    public ParameterModel(Domain.ParameterModel parameterModel)
        : this(parameterModel.Name, parameterModel.Type)
    { }
}
