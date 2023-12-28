namespace DomainModelOverReflection.Models.Domain;

#pragma warning disable IDE1006 // Naming Styles
public struct FieldModel
{
    public readonly string Name;
    public readonly string Type;
    public readonly bool IsPrivate;

    public FieldModel(string name, string type, bool ısPrivate)
    {
        Name = name;
        Type = type;
        IsPrivate = ısPrivate;
    }
}
#pragma warning restore IDE1006 // Naming Styles
