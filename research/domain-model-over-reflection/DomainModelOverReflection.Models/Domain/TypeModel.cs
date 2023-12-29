using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

public struct TypeModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public MethodModel[]? Constructors { get; set; }
    public FieldModel[]? Fields { get; set; }
    public MethodModel[]? Methods { get; set; }

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
