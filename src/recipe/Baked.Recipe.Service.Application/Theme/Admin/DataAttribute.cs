namespace Baked.Theme.Admin;

[AttributeUsage(AttributeTargets.Property)]
public class DataAttribute(string prop)
    : Attribute()
{
    public string Prop { get; set; } = prop;
    public string? Label { get; set; }
    public bool Visible { get; set; } = true;
    public int Order { get; set; } = 0;
}