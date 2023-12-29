using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct MethodModel
{
    public readonly string Name;
    public readonly string Target;
    public readonly string ReturnType;
    public readonly ParameterModel[]? Parameters;
    public readonly bool IsPublic;

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
#pragma warning restore IDE1006 // Naming Styles
