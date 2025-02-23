using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Conventions;

namespace Baked.CodingStyle.AddRemoveChild;

public class AddRemoveChildCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new PluralizeActionConvention(_when: action =>
                (action.Method == HttpMethod.Delete && action.RouteParts.Count >= 2) ||
                (action.Method == HttpMethod.Post && Regexes.StartsWithAddOrCreate.IsMatch(action.Name) && action.RouteParts.Count >= 2)
            ));
            builder.Conventions.Add(new OnlyEntityParameterIsInRouteForDeleteChildConvention());
            builder.Conventions.Add(new RemoveFromRouteConvention(["Add", "Create"]));
        });
    }
}