using Baked.Architecture;
using Baked.Ui;

namespace Baked;

public static class PanelParametersAreStatefulUxFeature
{
    public static IFeature<UxFeature.Configurator> PanelParametersAreStateful(this UxFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditParameterComponent<Select>(
                where: cc => cc.Path.EndsWith("data-panel", "inputs", "*", "component"),
                component: sb => sb.Schema.Stateful = true
            );
            conventions.EditParameterComponent<SelectButton>(
                where: cc => cc.Path.EndsWith("data-panel", "inputs", "*", "component"),
                component: sb => sb.Schema.Stateful = true
            );
        });
    });
}