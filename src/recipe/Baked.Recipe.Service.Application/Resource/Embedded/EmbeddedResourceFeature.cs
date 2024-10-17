using Baked.Architecture;

namespace Baked.Resource.Embedded;

public class EmbeddedResourceFeature(List<EmbeddedFileProviderDescriptor> _descriptors)
    : IFeature<ResourceConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureFileProviders(providers =>
        {
            foreach (var descriptor in _descriptors)
            {
                providers.Add(descriptor);
            }
        });
    }
}