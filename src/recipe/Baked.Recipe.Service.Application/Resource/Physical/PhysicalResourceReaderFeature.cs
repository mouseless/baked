using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Baked.Resource.Physical;

public class PhysicalResourceReaderFeature(List<string> _roots) : IFeature<ResourceConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            foreach (string root in _roots)
            {
                var provider = new PhysicalFileProvider(root);
                services.AddSingleton(provider);
                services.AddSingleton<IFileProvider>(provider);
            }

            services.AddSingleton<PhysicalResourceReader>();
            services.AddSingleton<IPhysicalResourceReader, PhysicalResourceReader>(forward: true);
        });
    }
}
