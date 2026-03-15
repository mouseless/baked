using Baked.CodingStyle;
using Baked.CodingStyle.AddRemoveChild;

namespace Baked;

public static class AddRemoveChildCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public AddRemoveChildCodingStyleFeature AddRemoveChild() =>
            new();
    }
}