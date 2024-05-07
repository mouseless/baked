using Do.CodingStyle;
using Do.CodingStyle.AddRemoveChild;

namespace Do;

public static class AddRemoveChildCodingStyleExtensions
{
    public static AddRemoveChildCodingStyleFeature AddRemoveChild(this CodingStyleConfigurator _) =>
        new();
}