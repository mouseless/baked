using Baked.Architecture;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainDatas;

using B = Baked.Ui.Components;

namespace Baked.Ux.ActionsAsDataPanels;

public class ActionsAsDataPanelsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component"),
                component: () => B.DataPanel()
            );
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("data-panel", "title"),
                schema: (c, cc) => MethodNameInline(c.Method, cc)
            );
            conventions.EditMethodComponent<DataPanel>(
                when: c => c.Method.GetAction().Method == HttpMethod.Get,
                component: (dp, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateSchema<Input>(cc.Drill("data-panel", "inputs"));
                        if (input is null) { continue; }

                        dp.Schema.Inputs.Add(input);
                    }
                }
            );
            conventions.EditParameterSchema<Label>(
                where: cc => cc.Path.EndsWith("data-panel", "inputs", "*", "label"),
                schema: (label, c, cc) =>
                {
                    var (_, l) = cc;

                    label.FloatOn(() => l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}