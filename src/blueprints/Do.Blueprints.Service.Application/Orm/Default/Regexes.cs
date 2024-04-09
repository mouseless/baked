using System.Text.RegularExpressions;

namespace Do.Orm.Default;

internal static partial class Regexes
{
    [GeneratedRegex(@"^SingleBy(?<Unique>.+)$", RegexOptions.None, "en-US")]
    public static partial Regex SingleByUniqueMethod();
}