namespace Baked.Caching;

public class ClientCache(string type)
    : Attribute()
{
    public string Type { get; set; } = type;
}