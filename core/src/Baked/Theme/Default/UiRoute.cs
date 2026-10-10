namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
public class UiRoute(string _path)
    : Attribute()
{
    public string Path { get; set; } = _path;
    public Dictionary<string, string> Params { get; init; } = [];
}