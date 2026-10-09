using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.Ui;
using Humanizer;

namespace Baked.Ux.InitializerParametersAreInPageTitle;

public class InitializerParametersAreInPageTitleUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditTypeComponent<TabbedPage>(
                when: c =>
                    c.Type.Has<TransientAttribute>() && c.Type.HasMembers() &&
                    !c.Type.Has<LocatableAttribute>(),
                component: (tp, c, cc) =>
                {
                    var members = c.Type.GetMembers();
                    var initializer =
                        members.Methods.Having<InitializerAttribute>().SingleOrDefault() ??
                        throw DiagnosticCode.RequiresInitializerAction.Exception(
                            $"{c.Type.Name} is a transient but doesn't have an initializer action." +
                            " Initializer is needed to render its inputs on page title in a tabbed page."
                        );

                    tp.Schema.Inputs.AddRange(
                        initializer
                            .DefaultOverload.Parameters
                            .Select(p => p.GenerateSchema<Input>(cc.Drill("tabbed-page", "inputs")))
                            .OfType<Input>()
                    );
                }
            );

            conventions.EditParameterSchema<Input>(
                where: cc => cc.Path.EndsWith("tabbed-page", "inputs"),
                schema: i => i.QueryBound = true
            );

            conventions.EditParameterSchema<Label>(
                where: cc => cc.Path.EndsWith("tabbed-page", "inputs", "*", "label"),
                schema: (label, c, cc) =>
                {
                    var (_, l) = cc;

                    label.FloatOn(() => l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}