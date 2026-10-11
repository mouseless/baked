using Baked.CodingStyle;
using Baked.CodingStyle.ExtensionViaLocatableInitializer;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class ExtensionViaLocatableInitializerCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ExtensionViaLocatableInitializerCodingStyleFeature ExtensionViaLocatableInitializer() =>
            new();
    }

    extension(TypeModel type)
    {
        public bool TryGetLocatableTypeFromExtension(DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
        {
            locatableType = default;

            if (!type.TryGetMetadata(out var metadata)) { return false; }
            if (!metadata.TryGet<LocatableExtension>(out var locatableExtension)) { return false; }

            locatableType = domain.Types[locatableExtension.LocatableType];

            return true;
        }
    }
}