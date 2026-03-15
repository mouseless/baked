using Baked.Architecture;
using Baked.Authorization;

namespace Baked;

public static class AuthorizationExtensions
{
    extension(List<IFeature> features)
    {
        public void AddAuthorization(FeatureFunc<AuthorizationConfigurator> configure) =>
            features.Add(configure(new()));
    }
}