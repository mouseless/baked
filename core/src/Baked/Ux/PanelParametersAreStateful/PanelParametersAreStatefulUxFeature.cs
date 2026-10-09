using Baked.Architecture;
using Baked.Ui;

namespace Baked.Ux.PanelParametersAreStateful;

public class PanelParametersAreStatefulUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddParameterComponentConfiguration<Select>(
                where: cc => cc.Path.EndsWith("data-panel", "inputs", "*", "component"),
                component: sb => sb.Schema.Stateful = true
            );
            conventions.AddParameterComponentConfiguration<SelectButton>(
                where: cc => cc.Path.EndsWith("data-panel", "inputs", "*", "component"),
                component: sb => sb.Schema.Stateful = true
            );
        });
    }
}