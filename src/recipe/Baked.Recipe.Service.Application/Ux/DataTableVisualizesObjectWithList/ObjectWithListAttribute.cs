namespace Baked.Ux.DataTableVisualizesObjectWithList;

[AttributeUsage(AttributeTargets.Class)]
public class ObjectWithListAttribute(string listPropertyName)
    : Attribute()
{
    public string ListPropertyName { get; set; } = listPropertyName;
}