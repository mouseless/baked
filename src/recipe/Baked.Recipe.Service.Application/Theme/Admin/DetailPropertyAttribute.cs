namespace Baked.Theme.Admin;

[AttributeUsage(AttributeTargets.Property)]
public class DetailPropertyAttribute(string key, string title) : Attribute
{
    public string Key { get; } = key;
    public string Title { get; } = title;
}