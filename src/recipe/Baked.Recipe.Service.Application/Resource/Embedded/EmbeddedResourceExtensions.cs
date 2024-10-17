using Baked.Resource;
using Baked.Resource.Embedded;

namespace Baked;

public static class EmbeddedResourceExtensions
{
    public static EmbeddedResourceFeature EmbeddedResource(this ResourceConfigurator _, List<EmbeddedFileProviderDescriptor> descriptors) =>
        new(descriptors);
}