using System.Reflection;
using System.Text.RegularExpressions;

namespace Baked.Core;

internal static partial class Regexes
{
    [GeneratedRegex(@"[\s\S]*?(?=.Application|$)")]
    public static partial Regex AssemblyNameBeforeApplicationSuffix { get; }

    extension(Assembly assembly)
    {
        public string GetNameBeforeApplicationSuffix() =>
            AssemblyNameBeforeApplicationSuffix
                .Match(assembly.FullName ?? string.Empty)
                .Value;
    }

    [GeneratedRegex(@"^\w+ => \w+\.")]
    public static partial Regex LambdaOfASingleMemberAccessExpression { get; }

    extension(string expression)
    {
        public string StripLambdaFromASingleMemberAccessExpression() =>
            LambdaOfASingleMemberAccessExpression.Replace(expression, string.Empty);
    }
}