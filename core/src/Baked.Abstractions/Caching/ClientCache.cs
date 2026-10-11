namespace Baked.Caching;

[AttributeUsage(AttributeTargets.Method)]
public class ClientCache(string type)
    : Attribute()
{
    public string Type { get; set; } = type;
}