using Baked.Architecture;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class NamespaceAsRouteCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new UseNamespaceForBaseRouteConvention(), order: int.MaxValue - 10);
        });
    }
}