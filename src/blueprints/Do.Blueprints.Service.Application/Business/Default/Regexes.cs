using System.Text.RegularExpressions;

namespace Do.Business.Default;

internal static partial class Regexes
{
    [GeneratedRegex(@"^(Get|By|SingleBy|FirstBy).*$", RegexOptions.None, "en-US")]
    public static partial Regex GetMethod();

    [GeneratedRegex(@"^(Delete|Remove).*$", RegexOptions.None, "en-US")]
    public static partial Regex DeleteMethod();

    [GeneratedRegex(@"^(Update|Change|Set)$", RegexOptions.None, "en-US")]
    public static partial Regex PutMethod();

    [GeneratedRegex(@"^(Update|Change|Set).*$", RegexOptions.None, "en-US")]
    public static partial Regex PatchMethod();
}
