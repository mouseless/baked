using Baked.Architecture;
using Baked.CodingStyle.NamespaceAsRoute;
using Baked.Domain.Configuration;

namespace Baked;

public static class NamespaceAsRouteCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> NamespaceAsRoute(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new UseNamespaceForBaseRouteConvention(), order: Order.At.Max);
        });
    });
}