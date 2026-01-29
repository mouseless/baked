using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.Theme.Default;

namespace Baked.Playground.Override.Ui;

public class ParentWrapperUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.PropertyType.Is<ParentWrapper>(),
                attribute: data => data.Visible = false
            );
        });
    }
}