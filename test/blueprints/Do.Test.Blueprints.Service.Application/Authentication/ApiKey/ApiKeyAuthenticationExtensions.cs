using Do.Authentication;
using Do.Test.Authentication.ApiKey;

namespace Do;

public static class ApiKeyAuthenticationExtensions
{
    public static ApiKeyAuthenticationFeature ApiKey(this AuthenticationConfigurator _) => new();
}