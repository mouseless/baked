using Baked.Architecture;
using Baked.Authorization;

namespace Baked;

public static class AuthorizationExtensions
{
    public static void AddAuthorization(this List<IFeature> features, Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>> configure) =>
        features.Add(configure(new()));
}