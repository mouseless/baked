using System.Reflection;

namespace Do.Domain.Model;

public record ConstructorModel(List<ParameterModel> Parameters)
{
    public ConstructorModel(ConstructorInfo constructorInfo)
        : this([])
    {
        foreach (var parameter in constructorInfo.GetParameters())
        {
            Parameters.Add(new(parameter));
        }
    }
}