using System.Text.RegularExpressions;

namespace Baked.RestApi;

internal static partial class Regexes
{
    [GeneratedRegex(@"^Get.*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithGet();

    [GeneratedRegex(@"^(Add|Create).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithAddOrCreate();

    [GeneratedRegex(@"^(Delete|Remove|Clear).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithDeleteRemoveOrClear();

    [GeneratedRegex(@"^(Update|Change|Set)$", RegexOptions.None, "en-US")]
    public static partial Regex IsUpdateChangeOrSet();

    [GeneratedRegex(@"^(Update|Change|Set).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithUpdateChangeOrSet();
}