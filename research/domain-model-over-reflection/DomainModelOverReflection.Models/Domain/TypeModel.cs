using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct TypeModel
{
    public readonly string Name;
    public readonly string Type;
    public readonly MethodModel[]? Constructors;
    public readonly FieldModel[]? Fields;
    public readonly MethodModel[]? Methods;

    public TypeModel(string name, string type, MethodModel[]? constructors, FieldModel[]? fields, MethodModel[]? methods)
    {
        Name = name;
        Type = type;
        Constructors = constructors;
        Fields = fields;
        Methods = methods;
    }

    public TypeModel(Type type)
    {
        Name = type.Name;
        Type = type.FullName ?? string.Empty;

        Constructors = type.GetConstructors().Select(c => new MethodModel(c)).ToArray();
        Fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(f => new FieldModel(f)).ToArray();

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ??
            Array.Empty<MethodInfo>();
        Methods = methods.Where(m => !m.IsSpecialName).Select(m => new MethodModel(m)).ToArray();
    }
}
#pragma warning restore IDE1006 // Naming Styles
