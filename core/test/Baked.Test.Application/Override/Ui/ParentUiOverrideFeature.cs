using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.Test.Orm;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Datas;

namespace Baked.Test.Override.Ui;

public class ParentUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
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

            // TODO move to ux
            builder.Conventions.AddTypeSchema(
                when: c => c.Type.Is<Parent>(),
                schema: () => Remote("/parents/{id}", o => o.Params = Computed.UseRoute("params"))
            );

            // other :thinking:
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Has<LocatableAttribute>(),
                where: cc => cc.Path.EndsWith("Title", "Actions", "**", nameof(IComponentDescriptor.Action)),
                schema: ra => ra.Params = Computed.UseRoute("params")
            );
        });
    }
}