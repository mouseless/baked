using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

public struct MethodModel
{
    public string Name { get; set; }
    public string Target { get; set; }
    public string ReturnType { get; set; }
    public ParameterModel[]? Parameters { get; set; }
    public bool IsPublic { get; set; }

    public MethodModel(string name, string target, string returnType, ParameterModel[]? parameters, bool isPublic)
    {
        Name = name;
        Target = target;
        ReturnType = returnType;
        Parameters = parameters;
        IsPublic = isPublic;
    }

    public MethodModel(ConstructorInfo constructorInfo)
    {
        Name = constructorInfo.Name;
        Target = constructorInfo.DeclaringType?.Name ?? string.Empty;
        ReturnType = Target;
        IsPublic = constructorInfo.IsPublic;
        Parameters = constructorInfo.GetParameters().Select(p => new ParameterModel(p)).ToArray();
    }

    public MethodModel(MethodInfo methodInfo)
    {
        Name = methodInfo.Name;
        Target = methodInfo.DeclaringType?.Name ?? string.Empty;
        ReturnType = methodInfo.ReturnType.FullName ?? string.Empty;
        IsPublic = methodInfo.IsPublic;
        Parameters = methodInfo.GetParameters().Select(p => new ParameterModel(p)).ToArray();
    }
}
