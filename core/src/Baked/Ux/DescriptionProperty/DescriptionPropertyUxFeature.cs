using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked;

public static class DescriptionPropertyUxFeature
{
    public static IFeature<UxFeature.Configurator> DescriptionProperty(this UxFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Property.Add<UiDescription>();
            builder.Index.Parameter.Add<UiDescription>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c => c.Property.Name.EndsWith("Description"),
                attribute: () => new UiDescription(),
                order: Order.At.Infra
            );

            conventions.SetParameterAttribute(
                when: c => c.Parameter.Name.Pascalize().EndsWith("Description"),
                attribute: () => new UiDescription(),
                order: Order.At.Infra
            );

            conventions.EditPropertySchema<Field>(
                when: c => c.Property.Has<UiDescription>(),
                schema: f => f.Wide = true
            );

            conventions.AddParameterComponent<Textarea>(
                when: c => c.Parameter.Has<UiDescription>()
            );
            conventions.EditParameterSchema<FormPage.InputGroup>(
                when: c => c.Parameter.Has<UiDescription>(),
                schema: f => f.Wide = true
            );

            conventions.AddPropertyComponent<Dialog>(
                when: c => c.Property.Has<UiDescription>(),
                where: cc => cc.Path.EndsWith("data-table", "columns", "*", "component")
            );
            conventions.AddPropertyComponent<Button>(
                when: c => c.Property.Has<UiDescription>(),
                where: cc => cc.Path.EndsWith("open")
            );
            conventions.EditPropertyComponent<Button>(
                when: c => c.Property.Has<UiDescription>(),
                where: cc => cc.Path.EndsWith("open"),
                component: (b, c, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Icon = "pi pi-eye";
                    b.Schema.Label = l(c.Property.Name.Titleize());
                }
            );
            conventions.EditPropertyComponent<Dialog>(
                component: d => d.Schema.Content.Data ??= Fetch.Context.Parent()
            );
        });
    });
}