using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.QueryViaPluralName;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class QueryViaPluralNameCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public QueryViaPluralNameCodingStyleFeature QueryViaPluralName(
            HashSet<string>? queryMethodNames = default,
            HashSet<string>? primaryParameterNames = default,
            HashSet<string>? takeParameterNames = default,
            HashSet<string>? skipParameterNames = default,
            HashSet<string>? sortingParameterNames = default
        )
        {
            queryMethodNames ??= ["By"];
            primaryParameterNames ??= ["searchText"];
            takeParameterNames ??= ["take"];
            skipParameterNames ??= ["skip"];
            sortingParameterNames ??= ["sort"];

            return new(
                queryMethodNames,
                primaryParameterNames,
                takeParameterNames,
                skipParameterNames,
                sortingParameterNames
            );
        }
    }

    extension(TypeModel type)
    {
        public bool TryGetQueryAttribute([NotNullWhen(true)] out QueryClass? queryAttribute)
        {
            queryAttribute = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out queryAttribute);
        }

        public bool TryGetLocatableType(DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
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
}