using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;

namespace Baked;

public static class ScopedViaSuffixCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> ScopedViaSuffix(this CodingStyleFeature.Configurator x,
        IEnumerable<string>? suffixes = default
    ) => x.Define(suffixes ?? ["Context"], (configurator, suffixes) =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new Scoped(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<Service>() &&
                    suffixes.Any(suffix => c.Type.Name.EndsWith(suffix)),
                order: Order.At.Infra
            );
        });
    });
}