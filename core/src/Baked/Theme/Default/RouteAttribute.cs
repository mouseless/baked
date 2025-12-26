namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RouteAttribute(string _path)
    : Attribute()
{
    public string Path { get; set; } = _path;
    public Dictionary<string, string> Params { get; init; } = [];
}