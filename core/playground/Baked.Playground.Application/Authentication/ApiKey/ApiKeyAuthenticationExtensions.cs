using Baked.Authentication;
using Baked.Playground.Authentication.ApiKey;

namespace Baked;

public static class ApiKeyAuthenticationExtensions
{
    public static ApiKeyAuthenticationFeature ApiKey(this AuthenticationConfigurator _) => new();
}