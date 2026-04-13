namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RouteAttribute(string _path)
    : Attribute()
{
    public string Path { get; set; } = _path;
    public Dictionary<string, string> Params { get; init; } = [];
}