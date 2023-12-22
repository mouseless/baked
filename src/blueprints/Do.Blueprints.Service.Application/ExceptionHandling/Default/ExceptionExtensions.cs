using System.Text.RegularExpressions;

namespace Do.ExceptionHandling;

public static class ExceptionExtensions
{
    public static string ExceptionName(this Exception source)
    {
        string formattedName = Regex.Replace(source.GetType().Name, @"(\B[A-Z])", " $1");
        formattedName = Regex.Replace(formattedName, @" Exception$", string.Empty);

        return formattedName;
    }

    public static string ExceptionId(this Exception source)
    {
        string formattedName = source.ExceptionName();
        formattedName = Regex.Replace(formattedName, @"\s+", "-").ToLower();

        return formattedName;
    }
}
