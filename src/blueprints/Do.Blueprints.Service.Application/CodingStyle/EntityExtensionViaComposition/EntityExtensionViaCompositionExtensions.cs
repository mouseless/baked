using Do.CodingStyle;
using Do.CodingStyle.EntityExtensionViaComposition;

namespace Do;

public static class EntityExtensionViaCompositionExtensions
{
    public static EntityExtensionViaCompositionFeature EntityExtensionViaComposition(this CodingStyleConfigurator _) =>
        new();
}