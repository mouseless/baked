using Baked.CodingStyle;
using Baked.CodingStyle.AddRemoveChild;

namespace Baked;

public static class AddRemoveChildCodingStyleExtensions
{
    public static AddRemoveChildCodingStyleFeature AddRemoveChild(this CodingStyleConfigurator _) =>
        new();
}