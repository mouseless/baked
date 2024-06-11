using Baked.CodingStyle;
using Baked.CodingStyle.UriReturnIsRedirect;

namespace Baked;

public static class UriReturnIsRedirectCodingStyleExtensions
{
    public static UriReturnIsRedirectCodingStyleFeature UriReturnIsRedirect(this CodingStyleConfigurator _) =>
        new();
}