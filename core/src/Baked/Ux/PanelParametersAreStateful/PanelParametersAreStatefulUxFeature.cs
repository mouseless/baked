using Baked.Architecture;
using Baked.Ui;

namespace Baked.Ux.PanelParametersAreStateful;

public class PanelParametersAreStatefulUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddParameterComponentConfiguration<Select>(
                component: sb => sb.Schema.Stateful = true,
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Parameters), "*", nameof(Parameter.Component))
            );
            builder.Conventions.AddParameterComponentConfiguration<SelectButton>(
                component: sb => sb.Schema.Stateful = true,
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Parameters), "*", nameof(Parameter.Component))
            );
        });
    }
}