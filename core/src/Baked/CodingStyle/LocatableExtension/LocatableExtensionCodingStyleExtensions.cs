using Baked.CodingStyle;
using Baked.CodingStyle.LocatableExtension;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class LocatableExtensionCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public LocatableExtensionCodingStyleFeature LocatableExtension() =>
            new();
    }

    extension(TypeModel type)
    {
        public bool TryGetLocatableTypeFromExtension(DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
        {
            locatableType = default;

            if (!type.TryGetMetadata(out var locatableExtensionMetadata)) { return false; }
            if (!locatableExtensionMetadata.TryGet<LocatableExtensionAttribute>(out var locatableExtensionAttribute)) { return false; }

            locatableType = domain.Types[locatableExtensionAttribute.LocatableType];

            return true;
        }
    }
}