using Baked.Architecture;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Ux.PropertiesAsFieldset;

public class PropertiesAsFieldsetUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage), nameof(SimplePage.Contents));

                    var content = c.Type.GetSchema<Content>(cc.Drill("Fields"));
                    if (content is null) { return; }

                    sp.Schema.Contents.Add(content);
                },
                order: -10
            );
            builder.Conventions.AddTypeSchema(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("Fields"),
                schema: (c, cc) => TypeContent(c.Type, cc, "fields")
            );
            builder.Conventions.AddTypeComponent(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("Fields", nameof(Content.Component)),
                component: (c, cc) => TypeFieldset(c.Type.GetMembers(), cc)
            );
            builder.Conventions.AddTypeComponentConfiguration<Fieldset>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (f, c, cc) =>
                {
                    cc = cc.Drill(nameof(Fieldset), nameof(Fieldset.Fields));

                    foreach (var property in c.Type.GetMembers().Properties.GetDataProperties())
                    {
                        var field = property.GetSchema<Field>(cc.Drill(f.Schema.Fields.Count));
                        if (field is null) { continue; }

                        f.Schema.Fields.Add(field);
                    }
                }
            );
            builder.Conventions.AddPropertySchema(
                schema: (c, cc) => PropertyField(c.Property, cc)
            );
            builder.Conventions.AddPropertySchemaConfiguration<Field>(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (f, c) =>
                {
                    var prop = c.Property.Get<DataAttribute>().Prop;

                    f.Component.Data = Context.Parent(options: cd => cd.Prop = $"data.{prop}");
                }
            );
        });
    }
}