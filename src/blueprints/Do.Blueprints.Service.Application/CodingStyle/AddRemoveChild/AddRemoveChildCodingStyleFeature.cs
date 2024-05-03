using Do.Architecture;
using Do.RestApi.Conventions;

namespace Do.CodingStyle.AddRemoveChild;

public class AddRemoveChildCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new RemoveFromRouteConvention(["Add"], _pluralize: true));
            conventions.Add(new PluralizeActionForDeleteChildConvention());
            conventions.Add(new FirstParameterIsInRouteForDeleteChildConvention());
        });
    }
}