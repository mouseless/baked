using Baked.Ux;
using Baked.Ux.ActionsAreContents;

namespace Baked;

public static class ActionsAreContentsUxExtensions
{
    public static ActionsAreContentsUxFeature ActionsAreContents(this UxConfigurator _) =>
        new();
}