using Baked.Ux;
using Baked.Ux.ActionsAsDataPanels;

namespace Baked;

public static class ActionsAsDataPanelsUxExtensions
{
    extension(UxConfigurator _)
    {
        public ActionsAsDataPanelsUxFeature ActionsAsDataPanels() =>
            new();
    }
}