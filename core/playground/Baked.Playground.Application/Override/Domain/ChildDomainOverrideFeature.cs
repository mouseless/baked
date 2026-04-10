using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.RestApi;
using Baked.Theme.Default;

namespace Baked.Playground.Override.Domain;

public class ChildDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddLocateAction<Child>();

            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Type.Is<Child>() && c.Property.PropertyType.Is<ParentWrapper>(),
                attribute: data => data.Visible = false
            );

            builder.Conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Child>(),
                order: RestApiLayer.MaxConventionOrder + 15
            );
        });
    }
}