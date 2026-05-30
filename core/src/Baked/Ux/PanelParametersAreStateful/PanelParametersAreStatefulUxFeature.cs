using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Ui;

namespace Baked.Ux.PanelParametersAreStateful;

public class PanelParametersAreStatefulUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddParameterComponentConfiguration<Select>(
                component: sb => sb.Schema.Stateful = true,
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Inputs), "*", nameof(Input.Component)),
                order: Order.At.Ux
            );
            conventions.AddParameterComponentConfiguration<SelectButton>(
                component: sb => sb.Schema.Stateful = true,
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Inputs), "*", nameof(Input.Component)),
                order: Order.At.Ux
            );
        });
    }
}