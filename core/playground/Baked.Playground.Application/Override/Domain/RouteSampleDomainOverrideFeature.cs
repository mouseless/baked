using Baked.Architecture;
using Baked.Playground.CodingStyle.NamespaceAsRoute;

namespace Baked.Playground.Override.Domain;

public class RouteSampleDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.AddLocateAction<RouteSample>();
        });
    }
}