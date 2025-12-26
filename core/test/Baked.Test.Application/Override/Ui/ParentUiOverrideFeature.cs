using Baked.Architecture;
using Baked.Test.Orm;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

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
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: (sp) => sp.Data = Remote("/parents/{id}", o => o.Params = Computed.UseRoute("params"))
            );
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage.Contents));

                    foreach (var property in c.Type.GetMembers().Properties)
                    {
                        var content = property.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
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