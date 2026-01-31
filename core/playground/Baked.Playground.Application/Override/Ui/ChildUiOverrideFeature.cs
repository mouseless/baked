using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.RestApi;
using Baked.Theme.Default;

namespace Baked.Playground.Override.Ui;

public class ChildUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
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