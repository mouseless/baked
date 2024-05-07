using Do.Architecture;
using Do.RestApi.Conventions;

namespace Do.CodingStyle.AddRemoveChild;

public class AddRemoveChildCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new PluralizeActionConvention(_when: c =>
                (c.Action.Method == HttpMethod.Delete && c.Action.RouteParts.Count >= 2) ||
                (c.Action.Method == HttpMethod.Post && c.Action.Name.StartsWith("Add") && c.Action.RouteParts.Count >= 2)
            ));
            conventions.Add(new FirstParameterIsInRouteForDeleteChildConvention());
            conventions.Add(new RemoveFromRouteConvention(["Add"]));
        });
    }
}