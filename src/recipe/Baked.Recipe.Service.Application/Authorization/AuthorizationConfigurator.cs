using Baked.Architecture;

namespace Baked.Authorization;

public class AuthorizationConfigurator
{
    public IFeature<AuthorizationConfigurator> Disabled() =>
        Feature.Empty<AuthorizationConfigurator>();
}