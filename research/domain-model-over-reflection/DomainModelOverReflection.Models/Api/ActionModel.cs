using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ActionModel(string Route, HttpMethod Method, string ReturnType, List<ParameterModel> Parameters)
{
    public ActionModel(MethodInfo methodInfo, HttpMethod? httpMethod = default)
        : this($"{methodInfo.DeclaringType?.Name}/{methodInfo.Name}", HttpMethod.Get, string.Empty, new())
    {
        Method = httpMethod ?? (methodInfo.Name.StartsWith("Delete") ? HttpMethod.Delete : methodInfo.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post);
        ReturnType = methodInfo.ReturnType.FullName ?? string.Empty;

        var parameters = methodInfo.GetParameters() ?? Array.Empty<ParameterInfo>();

        foreach (var parameter in parameters)
        {
            Parameters.Add(new(parameter));
        }
    }

    public ActionModel(MethodModel methodModel, HttpMethod? httpMethod = default)
        : this(string.Empty, HttpMethod.Get, methodModel.ReturnType, new())
    {
        Method = httpMethod ?? (methodModel.Name.StartsWith("Delete") ? HttpMethod.Delete : methodModel.Name.StartsWith("Edit") ? HttpMethod.Put : HttpMethod.Post);
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
