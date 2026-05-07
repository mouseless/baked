using Baked.CodingStyle;
using Baked.CodingStyle.QueryMethod;

namespace Baked;

public static class QueryMethodCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public QueryMethodCodingStyleFeature QueryMethod(
            HashSet<string>? queryMethodNames = default,
            HashSet<string>? takeParameterNames = default,
            HashSet<string>? skipParameterNames = default,
            HashSet<string>? sortingParameterNames = default
        )
        {
            queryMethodNames ??= ["By"];
            takeParameterNames ??= ["take"];
            skipParameterNames ??= ["skip"];
            sortingParameterNames ??= ["sort"];

            return new(queryMethodNames, takeParameterNames, skipParameterNames, sortingParameterNames);
        }
    }
}