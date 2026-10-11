using Baked.Architecture;
using Baked.Ui;
using Humanizer;

namespace Baked.Ux.ActionsAsDataPanels;

public class ActionsAsDataPanelsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddMethodComponent<DataPanel>(
                where: cc => cc.Path.EndsWith("contents", "*", "*", "component")
            );
            conventions.AddMethodSchema<InlineData>(
                where: cc => cc.Path.EndsWith("data-panel", "title")
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
            conventions.EditParameterSchema<Labeler>(
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