namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct TypeModel
{
    public readonly string Name;
    public readonly string Type;
    public readonly MethodModel[] Constructors;
    public readonly FieldModel[] Fields;
    public readonly MethodModel[] Methods;

    public TypeModel(string name, string type, MethodModel[] constructors, FieldModel[] fields, MethodModel[] methods)
    {
        Name = name;
        Type = type;
        Constructors = constructors;
        Fields = fields;
        Methods = methods;
    }
}
