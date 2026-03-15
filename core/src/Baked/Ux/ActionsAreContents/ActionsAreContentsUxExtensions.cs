using Baked.Ux;
using Baked.Ux.ActionsAreContents;

namespace Baked;

public static class ActionsAreContentsUxExtensions
{
    extension(UxConfigurator _)
    {
        public ActionsAreContentsUxFeature ActionsAreContents() =>
            new();
    }
}