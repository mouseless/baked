using Baked.Architecture;
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
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: dt =>
                {
                    dt.Schema.Rows = 5;
                    dt.Schema.Paginator = true;
                }
            );

            // Columns
            builder.Conventions.AddPropertySchema(
                when: c => c.Property.Has<DataAttribute>(),
                schema: (c, cc) => PropertyDataTableColumn(c.Property, cc)
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var (_, l) = cc;
                    var data = c.Property.Get<DataAttribute>();

                    dtc.Title = data.Label is not null ? l(data.Label) : null;
                    dtc.Exportable = true;
                }
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: (dtc, c, cc) =>
                {
                    var data = c.Property.Get<DataAttribute>();

                    var rootProp = cc.Path.Contains(nameof(DataTable.FooterTemplate)) ? "data" : "row";
                    dtc.Component.Data ??= Context.Parent(options: o => o.Prop = $"{rootProp}.{data.Prop}");
                },
                order: UiLayer.MaxConventionOrder - 10
            );

            // Export
            builder.Conventions.AddMethodSchema(
                when: c => c.Method.Has<ComponentDescriptorBuilderAttribute<DataTable>>(),
                schema: (c, cc) => MethodDataTableExport(c.Method, cc),
                order: 10
            );

            // Actions
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.Contains(nameof(DataTable), nameof(DataTable.Actions)),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row")
            );

            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
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

            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (col, c, cc) =>
                {
                    col.Frozen = true;
                    col.AlignRight = true;
                    col.Exportable = false;
                }
            );

            // `Button` defaults
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc =>
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "*") ||
                    cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions), "**", nameof(SimpleForm.DialogOptions.Open)),
                component: ButtonDefaults,
                order: 10
            );
            builder.Conventions.AddPropertyComponentConfiguration<Button>(
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