namespace Baked.Ux.ObjectWithListIsDataTable;

[AttributeUsage(AttributeTargets.Class)]
public class ObjectWithList(string listPropertyName)
    : Attribute()
{
    public string ListPropertyName { get; set; } = listPropertyName;
}