namespace Baked.Theme.Admin;

public class TabNameAttribute
    : Attribute
{
    public string Value { get; set; } = "Default";
}