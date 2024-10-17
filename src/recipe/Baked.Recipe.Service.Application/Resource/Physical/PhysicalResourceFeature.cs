using Baked.Architecture;

namespace Baked.Resource.Physical;

public class PhysicalResourceFeature(List<PhysicalFileProviderDescriptor> _descriptors) : IFeature<ResourceConfigurator>
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