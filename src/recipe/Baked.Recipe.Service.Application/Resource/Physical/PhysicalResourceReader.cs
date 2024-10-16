using Microsoft.Extensions.FileProviders;

namespace Baked.Resource.Physical;

public class PhysicalResourceReader(IEnumerable<PhysicalFileProvider> _providers)
    : ResourceReaderBase(_providers), IPhysicalResourceReader;

