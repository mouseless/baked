using Do.Architecture;

namespace Do.Authorization;

public class AuthorizationConfigurator
{
    public IFeature<AuthorizationConfigurator> Disabled() =>
        Feature.Empty<AuthorizationConfigurator>();
}