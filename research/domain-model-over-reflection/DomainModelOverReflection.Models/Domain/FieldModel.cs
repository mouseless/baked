using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct FieldModel
{
    public readonly string Name;
    public readonly string Type;
    public readonly bool IsPrivate;

    public FieldModel(string name, string type, bool isPrivate)
    {
        Name = name;
        Type = type;
        IsPrivate = isPrivate;
    }

    public FieldModel(FieldInfo fieldInfo)
    {
        Name = fieldInfo.Name;
        Type = fieldInfo.FieldType.Name;
        IsPrivate = fieldInfo.IsPrivate;
    }
}
#pragma warning restore IDE1006 // Naming Styles
