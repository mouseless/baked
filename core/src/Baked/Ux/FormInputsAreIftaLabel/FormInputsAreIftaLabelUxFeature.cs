using Baked.Architecture;
using Baked.Ui;
using Humanizer;

namespace Baked.Ux.FormInputsAreIftaLabelUxExtensions;

public class FormInputsAreIftaLabelUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddParameterSchemaConfiguration<Label>(
                where: cc =>
                    cc.Path.EndsWith("simple-form", "inputs", "*", "label") ||
                    cc.Path.EndsWith("form-page", "**", "inputs", "*", "label"),
                schema: (label, c, cc) =>
                {
                    var (_, l) = cc;

                    label.Ifta(() => l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}