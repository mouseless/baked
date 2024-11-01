using System.Reflection;
using System.Text.RegularExpressions;

namespace Baked.Core;

internal static partial class Regexes
{
    [GeneratedRegex(@"[\s\S]*?(?=.Application|$)")]
    public static partial Regex AssemblyNameBeforeApplicationSuffix();

    public static string GetNameBeforeApplicationSuffix(this Assembly assembly) =>
        AssemblyNameBeforeApplicationSuffix()
            .Match(assembly.FullName ?? string.Empty)
            .Value;
}