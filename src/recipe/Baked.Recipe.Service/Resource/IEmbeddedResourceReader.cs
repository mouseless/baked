namespace Baked.Resource;

public interface IEmbeddedResourceReader
{
    string? ReadAsString(string subPath);
    Task<string?> ReadAsStringAsync(string subPath);
}