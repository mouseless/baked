using System.Text.RegularExpressions;

namespace Do.RestApi;

internal static partial class Regexes
{
    [GeneratedRegex(@"^Get.*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithGet();

    [GeneratedRegex(@"^(Delete|Remove|Clear).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithDeleteRemoveOrClear();

    [GeneratedRegex(@"^(Update|Change|Set)$", RegexOptions.None, "en-US")]
    public static partial Regex IsUpdateChangeOrSet();

    [GeneratedRegex(@"^(Update|Change|Set).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithUpdateChangeOrSet();

    [GeneratedRegex("(?<=[a-z])(?=[A-Z])")]
    public static partial Regex MatchUpperInitial();
}