using Do.Architecture;
using Do.Business.Attributes;
using Do.Lifetime;

namespace Do.CodingStyle.ScopedBySuffix;

public class ScopedBySuffixCodingStyleFeature(IEnumerable<string> _suffices)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddType(new ScopedAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMetadata(out var metadata) &&
                    metadata.Has<ServiceAttribute>() &&
                    _suffices.Any(suffix => type.Name.EndsWith(suffix))
            );
        });
    }
}