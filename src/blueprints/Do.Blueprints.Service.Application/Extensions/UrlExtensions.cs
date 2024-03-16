using Do.Testing;
using Shouldly;
using System.Text.RegularExpressions;

namespace Do;

public static class UrlExtensions
{
    public static Uri AUrl(this Stubber giveMe,
        string? url = default
    )
    {
        url ??= $"https://www.{Regex.Replace(giveMe.AGuid().ToString(), "[0-9]", "x")}.com";

        return new(url);
    }

    public static void ShouldBe(this Uri? uri, string urlString) => uri?.ToString().ShouldBe(urlString);
}
