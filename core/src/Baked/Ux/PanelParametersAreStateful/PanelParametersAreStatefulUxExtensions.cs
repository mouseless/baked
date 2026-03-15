using Baked.Ux;
using Baked.Ux.PanelParametersAreStateful;

namespace Baked;

public static class PanelParametersAreStatefulUxExtensions
{
    extension(UxConfigurator _)
    {
        public PanelParametersAreStatefulUxFeature PanelParametersAreStateful() =>
            new();
    }
}