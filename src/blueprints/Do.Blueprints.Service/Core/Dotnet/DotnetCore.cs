using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Blueprints.Service.Core.Dotnet;

public class DotnetCore : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ISystem, DotnetSystem>();
        });
    }
}