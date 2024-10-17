using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Core;

public class ResourceSamples(IFileProvider _provider)
{
    public string? ReadFromCompositeProvider(string subpath) =>
        _provider.ReadAsString(subpath);
}