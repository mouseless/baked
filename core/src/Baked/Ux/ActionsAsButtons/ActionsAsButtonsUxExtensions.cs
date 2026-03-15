using Baked.Ux;
using Baked.Ux.ActionsAsButtons;

namespace Baked;

public static class ActionsAsButtonsUxExtensions
{
    extension(UxConfigurator _)
    {
        public ActionsAsButtonsUxFeature ActionsAsButtons() =>
            new();
    }
}