using Baked.Architecture;
using Baked.RestApi;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class NamespaceAsRouteCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new UseNamespaceForBaseRouteConvention(), order: RestApiLayer.MaxConventionOrder - 10);
        });
    }
}