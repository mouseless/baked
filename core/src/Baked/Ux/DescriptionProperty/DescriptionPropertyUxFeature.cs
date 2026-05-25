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
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Property.Add<DescriptionAttribute>();
            builder.Index.Parameter.Add<DescriptionAttribute>();
        });

        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c => c.Property.Name.EndsWith("Description"),
                attribute: () => new DescriptionAttribute()
            );

            conventions.SetParameterAttribute(
                when: c => c.Parameter.Name.Pascalize().EndsWith("Description"),
                attribute: () => new DescriptionAttribute()
            );

            conventions.AddPropertySchemaConfiguration<Field>(
                when: c => c.Property.Has<DescriptionAttribute>(),
                schema: f => f.Wide = true
            );

            conventions.AddParameterSchemaConfiguration<FormPage.InputGroup>(
                when: c => c.Parameter.Has<DescriptionAttribute>(),
                schema: f => f.Wide = true
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Columns), "*", nameof(DataTable.Column.Component)),
                component: (c, cc) => PropertyDialog(c.Property, cc)
            );
            conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(Dialog.Open)),
                component: (c, cc) => LocalizedButton(c.Property.Name.Titleize(), cc)
            );
            conventions.AddPropertyComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith(nameof(Dialog.Open)),
                component: b => b.Schema.Icon = "pi pi-eye"
            );
            conventions.AddPropertyComponentConfiguration<Dialog>(
                component: d => d.Schema.Content.Data ??= Context.Parent()
            );
        });
    }
}