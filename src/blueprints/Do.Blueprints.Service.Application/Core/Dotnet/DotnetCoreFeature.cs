using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Core.Dotnet;

public class DotnetCoreFeature : ICoreFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ISystem, System>();
        });
    }
}