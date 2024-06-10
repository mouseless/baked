using Baked.Architecture;

namespace Baked.Authentication;

public class AuthenticationConfigurator
{
    public IFeature<AuthenticationConfigurator> Disabled() =>
        Feature.Empty<AuthenticationConfigurator>();
}