using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.RestApi;
using Baked.Theme.Default;

namespace Baked.Playground.Override.Domain;

public class ChildDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddLocateAction<Child>();

            conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Type.Is<Child>() && c.Property.PropertyType.Is<ParentWrapper>() || c.Property.PropertyType.Is<IParentInterface>(),
                attribute: data => data.Visible = false
            );

            conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Child>(),
                order: RestApiLayer.MaxConventionOrder + 15
            );
        });
    }
}