using System.Text.RegularExpressions;

namespace Baked.RestApi;

internal static partial class Regexes
{
    [GeneratedRegex(@"^Get.*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithGet { get; }

    [GeneratedRegex(@"^(Add|Create|New).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithAddCreateOrNew { get; }

    [GeneratedRegex(@"^(Delete|Remove|Clear).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithDeleteRemoveOrClear { get; }

    [GeneratedRegex(@"^(Update|Change|Set)$", RegexOptions.None, "en-US")]
    public static partial Regex IsUpdateChangeOrSet { get; }

    [GeneratedRegex(@"^(Update|Change|Set).*$", RegexOptions.None, "en-US")]
    public static partial Regex StartsWithUpdateChangeOrSet { get; }
}