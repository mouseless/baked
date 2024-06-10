using Do.Architecture;

namespace Do.Authentication;

public class AuthenticationConfigurator
{
    public IFeature<AuthenticationConfigurator> Disabled() =>
        Feature.Empty<AuthenticationConfigurator>();
}