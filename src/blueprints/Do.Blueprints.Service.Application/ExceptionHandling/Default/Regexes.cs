using System.Text.RegularExpressions;

namespace Do.ExceptionHandling.Default;

internal static partial class Regexes
{
    [GeneratedRegex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.None, "en-US")]
    public static partial Regex PascalCaseSplitter();
}