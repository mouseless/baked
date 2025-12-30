using Baked.Architecture;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Ux.DescriptionProperty;

public class DescriptionPropertyUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Property.Add<DescriptionAttribute>();
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.Name.EndsWith("Description"),
                attribute: () => new DescriptionAttribute()
            );
            builder.Conventions.AddPropertySchemaConfiguration<Field>(
                when: c => c.Property.Has<DescriptionAttribute>(),
                schema: f => f.Wide = true
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Columns), "*", nameof(DataTable.Column.Component)),
                component: (c, cc) => PropertyDialog(c.Property, cc)
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(Dialog.Open)),
                component: (c, cc) => LocalizedButton(c.Property.Name.Titleize(), cc)
            );
            builder.Conventions.AddPropertyComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith(nameof(Dialog.Open)),
                component: b => b.Schema.Icon = "pi pi-eye"
            );
            builder.Conventions.AddPropertyComponentConfiguration<Dialog>(
                component: d => d.Schema.Content.Data ??= Context.Parent()
            );
        });
    }
}