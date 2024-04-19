using Do.Architecture;
using Do.Authorization;

namespace Do;

public static class AuthorizationExtensions
{
    public static void AddAuthorization(this List<IFeature> source, Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>> configure) =>
        source.Add(configure(new()));
}