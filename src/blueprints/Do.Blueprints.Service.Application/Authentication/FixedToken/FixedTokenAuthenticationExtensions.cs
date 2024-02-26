using Do.Authentication;
using Do.Authentication.FixedToken;

namespace Do;

public static class FixedTokenAuthenticationExtensions
{
    public static FixedTokenAuthenticationFeature FixedToken(this AuthenticationConfigurator _, List<string> tokenNames) => new(tokenNames);
}
