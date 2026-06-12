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
                    cc.Path.EndsWith(nameof(SimpleForm), nameof(SimpleForm.Inputs), "*", nameof(ILabeler.Label)) ||
                    cc.Path.EndsWith(nameof(FormPage), "**", nameof(FormPage.InputGroup.Inputs), "*", nameof(ILabeler.Label)),
                schema: (label, c, cc) =>
                {
                    var (_, l) = cc;

                    label.Ifta(() => l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}