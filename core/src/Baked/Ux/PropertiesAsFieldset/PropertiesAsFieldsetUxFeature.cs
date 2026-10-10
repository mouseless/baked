using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.PropertiesAsFieldset;

public class PropertiesAsFieldsetUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditTypeComponent<SimplePage>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill("simple-page", "contents", sp.Schema.Contents.Count);

                    var content = c.Type.GenerateSchema<Content>(cc.Drill("fields"));
                    if (content is null) { return; }

                    sp.Schema.Contents.Add(content);
                },
                order: -10
            );
            conventions.AddTypeSchema(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("fields"),
                schema: () => B.Content()
            );
            conventions.AddTypeComponent(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("fields", "component"),
                component: () => B.Fieldset()
            );
            conventions.EditTypeComponent<Fieldset>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                component: (f, c, cc) =>
                {
                    cc = cc.Drill("fieldset", "fields");

                    foreach (var property in c.Type.GetMembers().Properties.GetDataProperties())
                    {
                        var field = property.GenerateSchema<Field>(cc.Drill(f.Schema.Fields.Count));
                        if (field is null) { continue; }

                        f.Schema.Fields.Add(field);
                    }
                }
            );
            conventions.AddPropertySchema(
                schema: () => B.Field()
            );
            conventions.EditPropertySchema<Field>(
                when: c =>
                    c.Property.Has<DataAttribute>() &&
                    c.Property.PropertyType.TryGetMembers(out var members) && members.Has<Locatable>(),
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();
                    var members = c.Property.PropertyType.GetMembers();
                    var labelProperty =
                        members.FirstPropertyOrDefault<Label>() ??
                        members.FirstProperty<IdProperty>();
                    var labelData = labelProperty.Get<DataAttribute>();

                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"data.{data.Prop}.{labelData.Prop}");
                }
            );
            conventions.EditPropertySchema<Field>(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (f, c) =>
                {
                    var prop = c.Property.Get<DataAttribute>().Prop;

                    f.Component.Data ??= Context.Parent(options: cd => cd.Prop = $"data.{prop}");
                },
                order: Order.At.Theme.Max
            );
        });
    }
}