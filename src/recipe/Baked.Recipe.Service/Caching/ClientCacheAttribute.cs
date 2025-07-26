namespace Baked.Caching;

public class ClientCacheAttribute(string type)
    : Attribute()
{
    public string Type { get; set; } = type;
}