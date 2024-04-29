using Do.Authentication;
using Do.Test.Authentication.Jwt;

namespace Do;

public static class JwtAuthenticationExtentions
{
    public static JwtAuthenticationFeature Jwt(this AuthenticationConfigurator _) =>
        new();
}