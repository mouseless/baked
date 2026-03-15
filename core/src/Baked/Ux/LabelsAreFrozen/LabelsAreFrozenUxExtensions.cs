using Baked.Ux;
using Baked.Ux.LabelsAreFrozen;

namespace Baked;

public static class LabelsAreFrozenUxExtensions
{
    extension(UxConfigurator _)
    {
        public LabelsAreFrozenUxFeature LabelsAreFrozen() =>
            new();
    }
}