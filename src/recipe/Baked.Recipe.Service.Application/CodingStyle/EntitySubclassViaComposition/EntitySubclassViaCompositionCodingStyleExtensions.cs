using Baked.CodingStyle;
using Baked.CodingStyle.EntitySubclassViaComposition;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class EntitySubclassViaCompositionCodingStyleExtensions
{
    public static EntitySubclassViaCompositionCodingStyleFeature EntitySubclassViaComposition(this CodingStyleConfigurator _) =>
        new();

    public static bool TryGetSubclassName(this TypeModel type, [NotNullWhen(true)] out string? subclassName)
    {
        subclassName = default;

        if (!type.TryGetMetadata(out var entitySubclassMetadata)) { return false; }
        if (!entitySubclassMetadata.TryGet<EntitySubclassAttribute>(out var entitySubclassAttribute)) { return false; }

        subclassName = entitySubclassAttribute.Name;

        return true;
    }

    public static bool TryGetEntityTypeFromSubclass(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? entityType)
    {
        entityType = default;

        if (!type.TryGetMetadata(out var entitySubclassMetadata)) { return false; }
        if (!entitySubclassMetadata.TryGet<EntitySubclassAttribute>(out var entitySubclassAttribute)) { return false; }

        entityType = domain.Types[entitySubclassAttribute.EntityType];

        return true;
    }
}