using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Core.Dotnet;

public class DotnetCoreFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ISystem, DotnetSystem>();
        });
    }
}