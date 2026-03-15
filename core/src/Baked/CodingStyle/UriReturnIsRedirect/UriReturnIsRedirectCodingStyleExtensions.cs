using Baked.CodingStyle;
using Baked.CodingStyle.UriReturnIsRedirect;

namespace Baked;

public static class UriReturnIsRedirectCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public UriReturnIsRedirectCodingStyleFeature UriReturnIsRedirect() =>
            new();
    }
}