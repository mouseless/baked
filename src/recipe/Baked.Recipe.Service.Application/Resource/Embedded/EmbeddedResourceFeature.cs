using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Resource.Embedded;

public class EmbeddedResourceFeature(List<(Assembly assembly, string? baseNameSpace)> _assemblies)
    : IFeature<ResourceConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            foreach (var (assembly, baseNameSpace) in _assemblies)
            {
                var provider = new EmbeddedFileProvider(assembly, baseNameSpace);
                services.AddSingleton(provider);
                services.AddSingleton<IFileProvider>(provider);
            }

            services.AddSingleton<EmbeddedResourceReader>();
            services.AddSingleton<IEmbeddedResourceReader, EmbeddedResourceReader>(forward: true);
        });
    }
}