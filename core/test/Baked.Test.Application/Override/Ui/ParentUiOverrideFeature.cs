using Baked.Architecture;
using Baked.RestApi;
using Baked.Test.Orm;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Test.Override.Ui;

public class ParentUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // TODO review in conventions
            builder.Conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.RemoveChild),
                order: RestApiLayer.MaxConventionOrder + 15
            );
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Type.Is<Child>() && c.Property.Name == nameof(Child.Parent),
                attribute: data => data.Visible = false
            );
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: sp => sp.Data = Remote("/parents/{id}", o => o.Params = Computed.UseRoute("params"))
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<Parent>(),
                where: cc => cc.Path.EndsWith("Title", "Actions", "**", nameof(IComponentDescriptor.Action)),
                schema: ra => ra.Params = Computed.UseRoute("params")
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.GetChildren),
                component: dt => dt.ReloadOn(nameof(Parent.AddChild).Kebaberize())
            );
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage.Contents));

                    var propertyContents = new List<Content>();
                    foreach (var property in c.Type.GetMembers().Properties)
                    {
                        var content = property.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        propertyContents.Add(content);
                    }

                    sp.Schema.Contents.InsertRange(0, propertyContents);
                }
            );
            builder.Conventions.AddPropertySchema(
                when: c => c.Type.Is<Parent>(),
                where: cc => cc.Path.EndsWith(nameof(Page), "*", nameof(SimplePage.Contents), "*"),
                schema: (c, cc) => PropertyContent(c.Property, cc)
            );
            builder.Conventions.AddPropertyComponentConfiguration<Text>(
                when: c => c.Type.Is<Parent>(),
                where: cc => cc.Path.EndsWith(nameof(Page), "*", nameof(SimplePage.Contents), "*", "*", nameof(Content.Component)),
                component: (t, c) => t.Data = Context.Parent(o => o.Prop = $"data.{c.Property.Get<DataAttribute>().Prop}")
            );
        });
        // END TODO
    }
}