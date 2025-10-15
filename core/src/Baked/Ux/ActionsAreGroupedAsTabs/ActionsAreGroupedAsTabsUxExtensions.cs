using Baked.Ux;
using Baked.Ux.ActionsAreGroupedAsTabs;

namespace Baked;

public static class ActionsAreGroupedAsTabsUxExtensions
{
    public static ActionsAreGroupedAsTabsUxFeature ActionsAreGroupedAsTabs(this UxConfigurator _) =>
        new();
}