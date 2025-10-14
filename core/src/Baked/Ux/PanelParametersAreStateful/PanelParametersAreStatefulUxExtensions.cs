using Baked.Ux;
using Baked.Ux.PanelParametersAreStateful;

namespace Baked;

public static class PanelParametersAreStatefulUxExtensions
{
    public static PanelParametersAreStatefulUxFeature PanelParametersAreStateful(this UxConfigurator _) =>
        new();
}