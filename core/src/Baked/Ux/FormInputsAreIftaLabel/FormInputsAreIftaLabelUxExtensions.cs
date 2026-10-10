using Baked.Ux;
using Baked.Ux.FormInputsAreIftaLabelUxExtensions;

namespace Baked;

public static class FormInputsAreIftaLabelUxExtensions
{
    extension(UxConfigurator _)
    {
        public FormInputsAreIftaLabelUxFeature FormInputsAreIftaLabel() =>
            new();
    }
}