namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct MethodModel
{
    public readonly string Name;
    public readonly string Target;
    public readonly string ReturnType;
    public readonly ParameterModel[] Parameters;
    public readonly bool IsPublic;

    public MethodModel(string name, string target, string returnType, ParameterModel[] parameters, bool isPublic)
    {
        Name = name;
        Target = target;
        ReturnType = returnType;
        Parameters = parameters;
        IsPublic = isPublic;
    }
}
#pragma warning restore IDE1006 // Naming Styles
