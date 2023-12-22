using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public record ActionModel(string Route, HttpMethod Method, Type ReturnType, List<ParameterModel> Parameters)
{
    public ActionModel(MethodInfo methodInfo)
        : this($"{methodInfo.Name}", methodInfo.ReturnType == typeof(void) ? HttpMethod.Post : HttpMethod.Get, methodInfo.ReturnType, new())
    {
        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }
}
