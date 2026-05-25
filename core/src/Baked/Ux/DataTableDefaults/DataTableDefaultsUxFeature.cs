using Baked.Architecture;
using Baked.Business;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Ux.DataTableDefaults;

public class DataTableDefaultsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );

            // Columns
            conventions.AddPropertySchema(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc)
            );
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c => c.Property.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<LocatableAttribute>(),
                schema: (dtc, c, cc) => dtc.Hidden = cc.Path.StartsWith(nameof(Page), c.Property.PropertyType.Name) ? true : null
            );
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var (_, l) = cc;
                    var data = c.Property.Get<DataAttribute>();

                    dtc.Title = data.Label is not null ? l(data.Label) : null;
                    dtc.Exportable = true;
                }
            );
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c => c.Property.PropertyType.TryGetMembers(out var members) && members.Has<LocatableAttribute>(),
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();
                    var members = c.Property.PropertyType.GetMembers();
                    var labelProperty =
                        members.FirstPropertyOrDefault<LabelAttribute>() ??
                        members.FirstProperty<IdAttribute>();
                    var labelData = labelProperty.Get<DataAttribute>();

                    var rootProp = cc.Path.Contains(nameof(DataTable.FooterTemplate)) ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{data.Prop}.{labelData.Prop}");
                }
            );
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();

                    var rootProp = cc.Path.Contains(nameof(DataTable.FooterTemplate)) ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{data.Prop}");
                },
                order: UiLayer.MaxConventionOrder - 10
            );

            // Export
            conventions.AddMethodSchema(
                when: c => c.Method.Has<ComponentGeneratorAttribute<DataTable>>(),
                schema: (c, cc) => MethodDataTableExport(c.Method, cc),
                order: 10
            );

            // Actions
            conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.Contains(nameof(DataTable), nameof(DataTable.Actions)),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row")
            );

            conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    if (dt.Schema.Actions is null) { return; }
                    if (dt.Schema.Actions.Component is not ComponentDescriptor<Composite> composite) { return; }

                    foreach (var component in composite.Schema.Parts)
                    {
                        if (component.Action is not RemoteAction remote) { continue; }
                        if (remote.PostAction is not PublishAction publish) { continue; }
                        if (publish.Event is null) { continue; }

                        dt.ReloadOn(publish.Event);
                    }
                }
            );

            conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (col, c, cc) =>
                {
                    col.Frozen = true;
                    col.AlignRight = true;
                    col.Exportable = false;
                }
            );

            // `Button` defaults
            conventions.AddMethodComponentConfiguration<Button>(
                where: cc =>
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "*") ||
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "**", nameof(SimpleForm.DialogOptions.Open)),
                component: ButtonDefaults,
                order: 10
            );
            conventions.AddPropertyComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Columns), "**", nameof(SimpleForm.DialogOptions.Open)),
                component: ButtonDefaults,
                order: 10
            );
            void ButtonDefaults(ComponentDescriptor<Button> button)
            {
                if (button.Schema.Icon is not null)
                {
                    button.Schema.Label = string.Empty;
                }

                button.Schema.Variant = "text";
                button.Schema.Rounded = true;
            }
        });
    }
}