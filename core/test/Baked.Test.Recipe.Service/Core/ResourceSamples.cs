using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Core;

public class ResourceSamples(IFileProvider _provider)
{
    public string? Read(string subPath) =>
        _provider.ReadAsString(subPath);

    public async Task<string?> ReadAsync(string subPath) =>
        await _provider.ReadAsStringAsync(subPath);
}