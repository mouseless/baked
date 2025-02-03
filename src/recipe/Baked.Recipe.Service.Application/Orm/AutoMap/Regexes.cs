using System.Text.RegularExpressions;

namespace Baked.Orm.AutoMap;

internal static partial class Regexes
{
    [GeneratedRegex(@"^(FirstBy|SingleBy|By).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithFirstBySingleByOrBy { get; }

    [GeneratedRegex(@"^SingleBy(?<Name>.+)$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithSingleBy { get; }
}