using Baked.Architecture;
using Baked.Business;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.RoutedTypesAsNavLinks;

public class RoutedTypesAsNavLinksUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // adds navlink to types with route
            conventions.AddTypeComponent(
                when: c => c.Type.Has<UiRoute>(),
                where: cc => cc.Path.EndsWith("data-table", "columns", "*", "component"),
                component: () => B.NavLink()
            );

            // renders property as navlink when it is a label property for the types that has route
            conventions.AddPropertyComponent(
                when: c => c.Type.Has<UiRoute>() && c.Property.Has<Label>(),
                where: cc => cc.Path.EndsWith("data-table", "columns", "*", "component"),
                component: (c, cc) => c.Type.GenerateRequiredComponent<NavLink>(cc)
            );

            // configures navlink in data table to use route params from row data
            conventions.EditPropertyComponent<NavLink>(
                when: c => c.Type.Has<UiRoute>(),
                where: cc => cc.Path.EndsWith("data-table", "columns", "*", "component"),
                component: (link, c) =>
                {
                    foreach (var (param, prop) in c.Type.Get<UiRoute>().Params)
                    {
                        link.Schema.Params += Context.Parent(options: o =>
                        {
                            o.Prop = $"row.{prop}";
                            o.TargetProp = param;
                        });
                    }
                }
            );
        });
    }
}