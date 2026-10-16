using Baked.Architecture;
using Baked.Ui;
using Humanizer;

namespace Baked;

public static class FormInputsAreIftaLabelUxFeature
{
    public static IFeature<UxFeature.Configurator> FormInputsAreIftaLabel(this UxFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditParameterSchema<Labeler>(
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
    });
}