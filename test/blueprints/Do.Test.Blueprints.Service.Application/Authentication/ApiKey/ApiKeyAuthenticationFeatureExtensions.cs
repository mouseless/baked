using Do.Authentication;

namespace Do;

public static class ApiKeyAuthenticationFeatureExtensions
{
    public static ApiKeyAuthenticationFeature ApiKey(this AuthenticationConfigurator _, string name,
        IEnumerable<string>? claims = default
    ) => new(new(name, claims ?? ["User"]));
}