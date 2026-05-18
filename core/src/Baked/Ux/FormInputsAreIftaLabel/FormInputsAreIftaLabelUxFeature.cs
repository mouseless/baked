using Baked.Architecture;
using Baked.Ui;
using Humanizer;

namespace Baked.Ux.FormInputsAreIftaLabelUxExtensions;

public class FormInputsAreIftaLabelUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddParameterSchemaConfiguration<Input>(
                where: cc =>
                    cc.Path.EndsWith(nameof(SimpleForm), nameof(SimpleForm.Inputs)) ||
                    cc.Path.EndsWith(nameof(FormPage), "**", nameof(FormPage.InputGroup.Inputs)),
                schema: (i, c, cc) =>
                {
                    if (i.Component.Schema is not ILabeler labeler) { return; }

                    var (_, l) = cc;

                    labeler.LabelIfta(labeler.Label ?? l(c.Parameter.Name.Titleize()));
                }
            );
        });
    }
}