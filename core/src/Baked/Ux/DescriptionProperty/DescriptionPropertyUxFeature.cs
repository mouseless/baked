using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.DescriptionProperty;

public class DescriptionPropertyUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Property.Add<DescriptionAttribute>();
            builder.Index.Parameter.Add<DescriptionAttribute>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c => c.Property.Name.EndsWith("Description"),
                attribute: () => new DescriptionAttribute(),
                order: Order.At.Infra
            );

            conventions.SetParameterAttribute(
                when: c => c.Parameter.Name.Pascalize().EndsWith("Description"),
                attribute: () => new DescriptionAttribute(),
                order: Order.At.Infra
            );

            conventions.AddPropertySchemaConfiguration<Field>(
                when: c => c.Property.Has<DescriptionAttribute>(),
                schema: f => f.Wide = true
            );

            conventions.AddParameterComponent(
                when: c => c.Parameter.Has<DescriptionAttribute>(),
                component: () => B.Textarea()
            );
            conventions.AddParameterSchemaConfiguration<FormPage.InputGroup>(
                when: c => c.Parameter.Has<DescriptionAttribute>(),
                schema: f => f.Wide = true
            );

            conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith("data-table", "columns", "*", "component"),
                component: () => B.Dialog()
            );
            conventions.AddPropertyComponent(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith("open"),
                component: () => B.Button()
            );
            conventions.AddPropertyComponentConfiguration<Button>(
                when: c => c.Property.Has<DescriptionAttribute>(),
                where: cc => cc.Path.EndsWith("open"),
                component: (b, c, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Icon = "pi pi-eye";
                    b.Schema.Label = l(c.Property.Name.Titleize());
                }
            );
            conventions.AddPropertyComponentConfiguration<Dialog>(
                component: d => d.Schema.Content.Data ??= Context.Parent()
            );
        });
    }
}