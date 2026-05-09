using Baked.CodingStyle;
using Baked.CodingStyle.QueryMethod;

namespace Baked;

public static class QueryMethodCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public QueryMethodCodingStyleFeature QueryMethod(
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
}