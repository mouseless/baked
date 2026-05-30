using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Ux.PropertiesAsFieldset;

public class PropertiesAsFieldsetUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage), nameof(SimplePage.Contents));

                    var content = c.Type.GenerateSchema<Content>(cc.Drill("Fields"));
                    if (content is null) { return; }

                    sp.Schema.Contents.Add(content);
                },
                order: Order.At.Ux - 10
            );
            conventions.AddTypeSchema(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("Fields"),
                schema: (c, cc) => TypeContent(c.Type, cc, "fields"),
                order: Order.At.Ux
            );
            conventions.AddTypeComponent(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("Fields", nameof(Content.Component)),
                component: (c, cc) => TypeFieldset(c.Type.GetMembers(), cc),
                order: Order.At.Ux
            );
            conventions.AddTypeComponentConfiguration<Fieldset>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (f, c, cc) =>
                {
                    cc = cc.Drill(nameof(Fieldset), nameof(Fieldset.Fields));

                    foreach (var property in c.Type.GetMembers().Properties.GetDataProperties())
                    {
                        var field = property.GenerateSchema<Field>(cc.Drill(f.Schema.Fields.Count));
                        if (field is null) { continue; }

                        f.Schema.Fields.Add(field);
                    }
                },
                order: Order.At.Ux
            );
            conventions.AddPropertySchema(
                schema: (c, cc) => PropertyField(c.Property, cc),
                order: Order.At.Ux
            );
            conventions.AddPropertySchemaConfiguration<Field>(
                when: c =>
                    c.Property.Has<DataAttribute>() &&
                    c.Property.PropertyType.TryGetMembers(out var members) && members.Has<LocatableAttribute>(),
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();
                    var members = c.Property.PropertyType.GetMembers();
                    var labelProperty =
                        members.FirstPropertyOrDefault<LabelAttribute>() ??
                        members.FirstProperty<IdAttribute>();
                    var labelData = labelProperty.Get<DataAttribute>();

                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"data.{data.Prop}.{labelData.Prop}");
                },
                order: Order.At.Ux
            );
            conventions.AddPropertySchemaConfiguration<Field>(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (f, c) =>
                {
                    var prop = c.Property.Get<DataAttribute>().Prop;

                    f.Component.Data ??= Context.Parent(options: cd => cd.Prop = $"data.{prop}");
                },
                order: Order.At.Theme.Max // TODO consider using Order.At.Ux
            );
        });
    }
}