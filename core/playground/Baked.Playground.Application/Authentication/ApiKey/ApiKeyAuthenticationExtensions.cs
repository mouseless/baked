using Baked.Authentication;
using Baked.Playground.Authentication.ApiKey;

namespace Baked;

public static class ApiKeyAuthenticationExtensions
{
    extension(AuthenticationConfigurator _)
    {
        public ApiKeyAuthenticationFeature ApiKey() => new();
    }
}