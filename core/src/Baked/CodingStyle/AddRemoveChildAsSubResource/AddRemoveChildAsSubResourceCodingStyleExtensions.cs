using Baked.CodingStyle;
using Baked.CodingStyle.AddRemoveChildAsSubResource;

namespace Baked;

public static class AddRemoveChildAsSubResourceCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public AddRemoveChildAsSubResourceCodingStyleFeature AddRemoveChildAsSubResource() =>
            new();
    }
}