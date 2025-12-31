using Baked.Architecture;
using Baked.RestApi;
using Baked.Test.Orm;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked.Test.Override.Ui;

public class ParentUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddEntityRemoteData<Parent>();
            builder.Conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.RemoveChild),
                order: RestApiLayer.MaxConventionOrder + 15
            );
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Type.Is<Child>() && c.Property.Name == nameof(Child.Parent),
                attribute: data => data.Visible = false
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.GetChildren),
                component: dt => dt.ReloadOn(nameof(Parent.AddChild).Kebaberize())
            );
            builder.Conventions.AddTypeComponentConfiguration<Fieldset>(
                when: c => c.Type.Is<Parent>(),
                component: dt => dt.ReloadOn(nameof(Parent.Update).Kebaberize())
            );
        });
    }
}