namespace Baked.Theme.Admin;

public class TableAttribute(string title, string path) : Attribute
{
    public string Title { get; } = title;
    public string Path { get; } = path;
    public List<string> Columns { get; init; } = [];
}