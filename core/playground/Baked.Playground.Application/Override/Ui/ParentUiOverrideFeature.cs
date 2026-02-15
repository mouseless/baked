using Baked.Architecture;
using Baked.Business;
using Baked.Playground.Orm;
using Baked.RestApi;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked.Playground.Override.Ui;

public class ParentUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddEntityRemoteData<Parent>();
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Type.Is<Parent>() && c.Property.Name == nameof(Parent.Surname),
                attribute: () => new LabelAttribute()
            );
            builder.Conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.RemoveChild),
                order: RestApiLayer.MaxConventionOrder + 15
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