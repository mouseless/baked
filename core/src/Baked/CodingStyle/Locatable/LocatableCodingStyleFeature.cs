using Baked.Architecture;
using Baked.RestApi;

namespace Baked.CodingStyle.Locatable;

public class LocatableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new AddIdParameterToRouteConvention(), order: RestApiLayer.MaxConventionOrder - 100);
            builder.Conventions.Add(new LookupLocatableParameterConvention(), order: RestApiLayer.MaxConventionOrder - 30);
            builder.Conventions.Add(new LookupLocatableParametersConvention(), order: RestApiLayer.MaxConventionOrder - 30);
            builder.Conventions.Add(new TargetFromLocatorConvention(), order: RestApiLayer.MaxConventionOrder - 20);
        });
    }
}