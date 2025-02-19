namespace Baked.Theme.Admin;

[AttributeUsage(AttributeTargets.Class)]
public class DetailAttribute(string name, string path) : Attribute
{
    public string Name { get; } = name;
    public string Path { get; } = path;
}