using Baked.CodingStyle;
using Baked.CodingStyle.LocatableExtensions;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class LocatableExtensionsCodingStyleExtensions
{
    public static LocatableExtensionsCodingStyleFeature LocatableExtension(this CodingStyleConfigurator _) =>
        new();

    public static bool TryGetLocatableTypeFromExtension(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
    {
        locatableType = default;

        if (!type.TryGetMetadata(out var locatableExtensionMetadata)) { return false; }
        if (!locatableExtensionMetadata.TryGet<LocatableExtensionAttribute>(out var locatableExtensionAttribute)) { return false; }

        locatableType = domain.Types[locatableExtensionAttribute.LocatableType];

        return true;
    }
}