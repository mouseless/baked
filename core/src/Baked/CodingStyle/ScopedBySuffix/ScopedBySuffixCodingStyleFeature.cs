using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;

namespace Baked.CodingStyle.ScopedBySuffix;

public class ScopedBySuffixCodingStyleFeature(IEnumerable<string> _suffixes)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(new ScopedAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<ServiceAttribute>() &&
                    _suffixes.Any(suffix => c.Type.Name.EndsWith(suffix))
            );
        });
    }
}