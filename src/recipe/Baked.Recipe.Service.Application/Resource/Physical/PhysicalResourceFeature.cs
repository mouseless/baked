using Baked.Architecture;

namespace Baked.Resource.Physical;

public class PhysicalResourceFeature(List<string> _roots) : IFeature<ResourceConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureFileProviders(providers =>
        {
            foreach (string root in _roots)
            {
                providers.AddPhysical(root);
            }
        });
    }
}