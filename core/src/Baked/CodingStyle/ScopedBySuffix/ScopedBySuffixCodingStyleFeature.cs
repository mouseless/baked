using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;

namespace Baked.CodingStyle.ScopedBySuffix;

public class ScopedBySuffixCodingStyleFeature(IEnumerable<string> _suffixes)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new Scoped(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<Service>() &&
                    _suffixes.Any(suffix => c.Type.Name.EndsWith(suffix)),
                order: Order.At.Infra
            );
        });
    }
}