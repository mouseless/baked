using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.AddRemoveChild;

public class AddRemoveChildCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: action =>
                {
                    var newName = action.Name.Pluralize();
                    action.RouteParts = action.RouteParts.Replace(action.Name, newName);
                    action.Name = newName;
                },
                when: (_, action) =>
                    (action.Method == HttpMethod.Delete && action.RouteParts.Count >= 2) ||
                    (action.Method == HttpMethod.Post && Regexes.StartsWithAddCreateOrNew.IsMatch(action.Name) && action.RouteParts.Count >= 2)
            );
            builder.Conventions.Add(new OnlyEntityParameterIsInRouteForDeleteChildConvention());
            builder.Conventions.Add(new RemoveFromRouteConvention(["Add", "Create", "New"]));
        });
    }
}