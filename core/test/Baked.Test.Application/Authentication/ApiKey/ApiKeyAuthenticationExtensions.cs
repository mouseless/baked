using Baked.Authentication;
using Baked.Test.Authentication.ApiKey;

namespace Baked;

public static class ApiKeyAuthenticationExtensions
{
    public static ApiKeyAuthenticationFeature ApiKey(this AuthenticationConfigurator _) => new();
}