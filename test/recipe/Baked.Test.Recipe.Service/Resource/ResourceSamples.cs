using Microsoft.Extensions.FileProviders;

namespace Baked.Test;

public class ResourceSamples(IFileProvider _provider, IEnumerable<IFileProvider> _providers)
{
    public string? ReadFromProvider(string subpath) =>
        _provider.ReadAsString(subpath);

    public IEnumerable<string?> ReadFromProviders(string subpath) =>
        _providers.Select(p => p.ReadAsString(subpath));
}
