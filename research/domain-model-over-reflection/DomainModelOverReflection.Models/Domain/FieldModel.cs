using System.Reflection;

namespace DomainModelOverReflection.Models.Domain;

public struct FieldModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsPrivate { get; set; }

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

