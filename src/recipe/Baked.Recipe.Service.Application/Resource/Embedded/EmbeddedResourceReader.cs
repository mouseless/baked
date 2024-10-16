using Microsoft.Extensions.FileProviders;

namespace Baked.Resource.Embedded;

public class EmbeddedResourceReader(IEnumerable<EmbeddedFileProvider> _providers)
    : ResourceReaderBase(_providers), IEmbeddedResourceReader;