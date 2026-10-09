using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.DataTableDefaults;

public class DataTableDefaultsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditMethodComponent<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );

            // Columns
            conventions.AddPropertySchema(
                when: c => c.Property.Has<DataAttribute>(),
                schema: () => B.DataTableColumn()
            );
            conventions.EditPropertySchema<DataTable.Column>(
                when: c => c.Property.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<LocatableAttribute>(),
                schema: (dtc, c, cc) => dtc.Hidden = cc.Path.StartsWith("page", c.Property.PropertyType.Name) ? true : null
            );
            conventions.EditPropertySchema<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var (_, l) = cc;
                    var data = c.Property.Get<DataAttribute>();

                    dtc.Title = data.Label is not null ? l(data.Label) : null;
                    dtc.Exportable = true;
                }
            );
            conventions.EditPropertySchema<DataTable.Column>(
                when: c => c.Property.PropertyType.TryGetMembers(out var members) && members.Has<LocatableAttribute>(),
                schema: (dtc, c, cc) =>
                {
                    var members = c.Property.PropertyType.GetMembers();
                    var labelProperty =
                        members.FirstPropertyOrDefault<LabelAttribute>() ??
                        members.FirstProperty<IdAttribute>();

                    var rootProp = cc.Path.Contains("footer-template") ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{c.Property.DataProp}.{labelProperty.DataProp}");
                }
            );
            conventions.EditPropertySchema<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();

                    var rootProp = cc.Path.Contains("footer-template") ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{data.Prop}");
                },
                order: Order.At.Theme.Max
            );

            // Export
            conventions.AddMethodSchema(
                when: c => c.Method.Has<ComponentGeneratorAttribute<DataTable>>(),
                schema: () => B.DataTableExport(),
                order: 10
            );

            // Actions
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.Contains("data-table", "actions"),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row"),
                order: 10
            );

            conventions.EditMethodComponent<DataTable>(
                component: dt =>
                {
                    if (dt.Schema.Actions is null) { return; }
                    if (dt.Schema.Actions.Component.Schema is not Composite composite) { return; }

                    foreach (var component in composite.Parts)
                    {
                        if (component.Action is not RemoteAction remote) { continue; }
                        if (remote.PostAction is not PublishAction publish) { continue; }
                        if (publish.Event is null) { continue; }

                        dt.ReloadOn(publish.Event);
                    }
                }
            );

            conventions.EditMethodSchema<DataTable.Column>(
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: (col, c, cc) =>
                {
                    col.Frozen = true;
                    col.AlignRight = true;
                    col.Exportable = false;
                }
            );

            // `Button` defaults
            conventions.EditMethodComponent<Button>(
                where: cc =>
                    cc.Path.EndsWith("data-table", "actions", "*") ||
                    cc.Path.EndsWith("data-table", "actions", "**", "open"),
                component: ButtonDefaults,
                order: 10
            );
            conventions.EditPropertyComponent<Button>(
                where: cc => cc.Path.EndsWith("data-table", "columns", "**", "open"),
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