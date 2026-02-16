using Baked.Ux;
using Baked.Ux.LabelsAreFrozen;

namespace Baked;

public static class LabelsAreFrozenUxExtensions
{
    public static LabelsAreFrozenUxFeature LabelsAreFrozen(this UxConfigurator _) =>
        new();
}