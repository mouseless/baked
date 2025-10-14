using Baked.Ux;
using Baked.Ux.ActionsAsDataPanels;

namespace Baked;

public static class ActionsAsDataPanelsUxExtensions
{
    public static ActionsAsDataPanelsUxFeature ActionsAsDataPanels(this UxConfigurator _) =>
        new();
}