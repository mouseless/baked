using Baked.Architecture;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class NamespaceAsRouteCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new UseNamespaceForBaseRouteConvention(), order: Order.At.Max);
        });
    }
}