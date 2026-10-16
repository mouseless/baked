using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;

namespace Baked;

public static class PropertiesAsFieldsetUxFeature
{
    public static IFeature<UxFeature.Configurator> PropertiesAsFieldset(this UxFeature.Configurator x) => x.Define(configurator =>
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
            conventions.AddTypeSchema<Content>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("fields")
            );
            conventions.AddTypeComponent<Fieldset>(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.GetDataProperties().Any(),
                where: cc => cc.Path.EndsWith("fields", "component")
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
            conventions.AddPropertySchema<Field>();
            conventions.EditPropertySchema<Field>(
                when: c =>
                    c.Property.Has<UiData>() &&
                    c.Property.PropertyType.TryGetMembers(out var members) && members.Has<Locatable>(),
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<UiData>();
                    var members = c.Property.PropertyType.GetMembers();
                    var labelProperty =
                        members.FirstPropertyOrDefault<Label>() ??
                        members.FirstProperty<IdProperty>();
                    var labelData = labelProperty.Get<UiData>();

                    dtc.Component.Data ??= Fetch.Context.Parent(options: o => o.Prop = $"data.{data.Prop}.{labelData.Prop}");
                }
            );
            conventions.EditPropertySchema<Field>(
                when: c => c.Property.Has<UiData>(),
                schema: (f, c) =>
                {
                    var prop = c.Property.Get<UiData>().Prop;

                    f.Component.Data ??= Fetch.Context.Parent(options: cd => cd.Prop = $"data.{prop}");
                },
                order: Order.At.Theme.Max
            );
        });
    });
}