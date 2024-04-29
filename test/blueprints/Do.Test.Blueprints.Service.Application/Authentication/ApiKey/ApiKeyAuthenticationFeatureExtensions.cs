using Do.Authentication;

namespace Do;

public static class ApiKeyAuthenticationFeatureExtensions
{
    public static ApiKeyAuthenticationFeature ApiKey(this AuthenticationConfigurator _) =>
        new();
}