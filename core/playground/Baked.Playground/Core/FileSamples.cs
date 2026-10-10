using Microsoft.Extensions.FileProviders;

namespace Baked.Playground.Core;

public class FileSamples(IFileProvider _provider)
{
    public string? Read(string subPath) =>
        _provider.ReadAsString(subPath);

    public async Task<string?> ReadAsync(string subPath) =>
        await _provider.ReadAsStringAsync(subPath);
}