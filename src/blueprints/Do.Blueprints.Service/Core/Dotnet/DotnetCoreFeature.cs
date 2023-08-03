using Do.Architecture;
using Do.Core;
using Do.Core.Dotnet;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Blueprints.Service.Core.Dotnet;

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