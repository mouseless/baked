using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public record ActionModel(string Route, HttpMethod Method, Type ReturnType, List<ParameterModel> Parameters)
{
    public ActionModel(MethodInfo methodInfo)
        : this($"{methodInfo.DeclaringType?.Name}/{methodInfo.Name}", HttpMethod.Get, methodInfo.ReturnType, new())
    {
        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        Method = methodInfo.Name.StartsWith("Delete") ? HttpMethod.Delete :
            methodInfo.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post;

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodInfo methodInfo, HttpMethod httpMethod)
        : this($"{methodInfo.Name}", httpMethod, methodInfo.ReturnType, new())
    {
        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodModel methodModel)
        : this($"{methodModel.Target.Name}/{methodModel.Name}", HttpMethod.Get, methodModel.ReturnType, new())
    {
        Method = methodModel.Name.StartsWith("Delete") ? HttpMethod.Delete :
            methodModel.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post;

        foreach (var parameter in methodModel.Parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodModel methodModel, HttpMethod httpMethod)
        : this($"{methodModel.Name}", httpMethod, methodModel.ReturnType, new())
    {
        foreach (var parameter in methodModel.Parameters)
        {
            Parameters.Add(new(parameter));
        }
    }
}
