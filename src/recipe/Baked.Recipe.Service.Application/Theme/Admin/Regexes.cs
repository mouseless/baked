using System.Text.RegularExpressions;

namespace Baked.Theme.Admin;

public static partial class Regexes
{
    [GeneratedRegex(@"^.*/data-panel/.*/content$", RegexOptions.None, "en-US")]
    public static partial Regex AnyDataPanelContent { get; }
}