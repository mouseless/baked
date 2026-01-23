using Baked.Architecture;

namespace Baked.CodingStyle.LocatableTransient;

public class LocatableTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new AddIdParameterToRouteConvention());
            builder.Conventions.Add(new LookupLocatableConvention(), order: 50);
            builder.Conventions.Add(new FindTargetFromLocatorConvention(), order: 50);
        });
    }
}
