using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ActionModel(string Route, HttpMethod Method, string ReturnType, List<ParameterModel> Parameters)
{
    public ActionModel(MethodInfo methodInfo)
        : this($"{methodInfo.DeclaringType?.Name}/{methodInfo.Name}", HttpMethod.Get, string.Empty, new())
    {
        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        ReturnType = methodInfo.ReturnType.FullName ?? string.Empty;
        Method = methodInfo.Name.StartsWith("Delete") ? HttpMethod.Delete :
            methodInfo.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post;

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodInfo methodInfo, HttpMethod httpMethod)
        : this($"{methodInfo.DeclaringType?.Name}/{methodInfo.Name}", httpMethod, string.Empty, new())
    {
        ReturnType = methodInfo.ReturnType.FullName ?? string.Empty;
        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodModel methodModel)
        : this(string.Empty, HttpMethod.Get, methodModel.ReturnType, new())
    {
        Method = methodModel.Name.StartsWith("Delete") ? HttpMethod.Delete :
            methodModel.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post;
        Route = $"{methodModel.Target[(methodModel.Target.LastIndexOf('.') + 1)..]}/{methodModel.Name}";

        if (methodModel.Parameters is not null)
        {
            foreach (var parameter in methodModel.Parameters)
            {
                Parameters.Add(new(parameter));
            }
        }
    }

    public ActionModel(MethodModel methodModel, HttpMethod httpMethod)
        : this(string.Empty, httpMethod, methodModel.ReturnType, new())
    {
        Route = $"{methodModel.Target[(methodModel.Target.LastIndexOf('.') + 1)..]}/{methodModel.Name}";
        if (methodModel.Parameters is not null)
        {
            foreach (var parameter in methodModel.Parameters)
            {
                Parameters.Add(new(parameter));
            }
        }
    }
}
