using Baked.Architecture;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;

namespace Baked.Ux.ActionsAsDataPanels;

public class ActionsAsDataPanelsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith("Contents", "*", "*", nameof(Content.Component)),
                component: (c, cc) => MethodDataPanel(c.Method, cc)
            );
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Title)),
                schema: (c, cc) => MethodNameInline(c.Method, cc)
            );
            conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Method.GetAction().Method == HttpMethod.Get,
                component: (dp, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        dp.Schema.Inputs.Add(
                            parameter.GenerateRequiredSchema<Input>(cc.Drill(nameof(DataPanel), nameof(DataPanel.Inputs)))
                        );
                    }
                }
            );
            conventions.AddParameterSchemaConfiguration<Input>(
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Inputs)),
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }

                    var (_, l) = cc;

                    labeler.LabelFloatOn(labeler.Label ?? l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}