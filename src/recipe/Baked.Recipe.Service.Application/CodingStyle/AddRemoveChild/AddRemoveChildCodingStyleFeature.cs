using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Conventions;

namespace Baked.CodingStyle.AddRemoveChild;

public class AddRemoveChildCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new PluralizeActionConvention(_when: c =>
                (c.Action.Method == HttpMethod.Delete && c.Action.RouteParts.Count >= 2) ||
                (c.Action.Method == HttpMethod.Post && Regexes.StartsWithAddOrCreate().IsMatch(c.Action.Name) && c.Action.RouteParts.Count >= 2)
            ));
            conventions.Add(new FirstParameterIsInRouteForDeleteChildConvention());
            conventions.Add(new RemoveFromRouteConvention(["Add", "Create"]));
        });
    }
}