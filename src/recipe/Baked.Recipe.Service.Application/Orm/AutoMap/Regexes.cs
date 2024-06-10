using System.Text.RegularExpressions;

namespace Do.Orm.AutoMap;

internal static partial class Regexes
{
    [GeneratedRegex(@"^(FirstBy|SingleBy|By).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithFirstBySingleByOrBy();

    [GeneratedRegex(@"^SingleBy(?<Name>.+)$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithSingleBy();
}