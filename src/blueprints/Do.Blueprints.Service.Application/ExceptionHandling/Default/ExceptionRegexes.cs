using System.Text.RegularExpressions;

namespace Do.ExceptionHandling.Default;

internal static partial class ExceptionRegexes
{
    [GeneratedRegex(@"\p{Lu}\p{Ll}*", RegexOptions.None, "en-US")]
    private static partial Regex WordsFromCapitalLetters();

    public static MatchCollection SplitWordsFromCapitalLetters(this string source) =>
        WordsFromCapitalLetters().Matches(source);
}
