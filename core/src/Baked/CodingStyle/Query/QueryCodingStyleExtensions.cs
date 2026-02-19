using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.Query;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class QueryCodingStyleExtensions
{
    public static QueryCodingStyleFeature Query(this CodingStyleConfigurator _) =>
        new();

    public static bool TryGetQueryAttribute(this TypeModel type, [NotNullWhen(true)] out QueryAttribute? queryAttribute)
    {
        queryAttribute = default;

        return
            type.TryGetMetadata(out var metadata) &&
            metadata.TryGet(out queryAttribute);
    }

    public static bool TryGetLocatableType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
    {
        if (!type.TryGetQueryAttribute(out var queryAttribute))
        {
            locatableType = default;

            return false;
        }

        locatableType = domain.Types[queryAttribute.LocatableType];

        return true;
    }
}