using Do.CodingStyle;
using Do.CodingStyle.EntityExtensionViaComposition;

namespace Do;

public static class EntityExtensionViaCompositionCodingStyleExtensions
{
    public static EntityExtensionViaCompositionCodingStyleFeature EntityExtensionViaComposition(this CodingStyleConfigurator _) =>
        new();
}