using Do.CodingStyle;
using Do.CodingStyle.UriReturnIsRedirect;

namespace Do;

public static class UriReturnIsRedirectCodingStyleExtensions
{
    public static UriReturnIsRedirectCodingStyleFeature UriReturnIsRedirect(this CodingStyleConfigurator _) =>
        new();
}