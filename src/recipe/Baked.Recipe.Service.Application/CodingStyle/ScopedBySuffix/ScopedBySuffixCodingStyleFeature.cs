using Do.Architecture;
using Do.Business;
using Do.Lifetime;

namespace Do.CodingStyle.ScopedBySuffix;

public class ScopedBySuffixCodingStyleFeature(IEnumerable<string> _suffixes)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new ScopedAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<ServiceAttribute>() &&
                    _suffixes.Any(suffix => c.Type.Name.EndsWith(suffix))
            );
        });
    }
}