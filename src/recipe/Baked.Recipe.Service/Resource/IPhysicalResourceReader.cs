namespace Baked.Resource;

public interface IPhysicalResourceReader
{
    string? ReadAsString(string subPath);
    Task<string?> ReadAsStringAsync(string subPath);
}