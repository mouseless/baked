using System.Text.RegularExpressions;

namespace Do.Orm.AutoMap;

internal static partial class Regexes
{
    [GeneratedRegex(@"^(By|FirstBy|SingleBy).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithByFirstByOrSingleBy();

    [GeneratedRegex(@"^SingleBy(?<Name>.+)$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithSingleBy();
}