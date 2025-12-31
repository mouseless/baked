using Baked.Ux;
using Baked.Ux.ActionsAsButtons;

namespace Baked;

public static class ActionsAsButtonsUxExtensions
{
    public static ActionsAsButtonsUxFeature ActionsAsButtons(this UxConfigurator _) =>
        new();
}