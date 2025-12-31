using Baked.Architecture;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

namespace Baked.Ux.RoutedTypesAsNavLinks;

public class RoutedTypesAsNavLinksUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyComponent(
                when: c => c.Type.Has<RouteAttribute>() && c.Property.Has<LabelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Columns), "*", nameof(DataTable.Column.Component)),
                component: (c, cc) => TypeNavLink(c.Type, cc)
            );
            builder.Conventions.AddPropertyComponentConfiguration<NavLink>(
                when: c => c.Type.Has<RouteAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Columns), "*", nameof(DataTable.Column.Component)),
                component: (link, c) =>
                {
                    foreach (var (param, prop) in c.Type.Get<RouteAttribute>().Params)
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