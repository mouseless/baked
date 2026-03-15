using Baked.Architecture;
using Baked.Authorization;

namespace Baked;

public static class AuthorizationExtensions
{
    extension(List<IFeature> features)
    {
        public void AddAuthorization(Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}