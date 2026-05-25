using Baked.Architecture;
using Baked.Playground.Authentication;

namespace Baked.Playground.Override.Domain;

public class AuthenticationSamplesDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.AddConfigureAction<AuthenticationSamples>(nameof(AuthenticationSamples.FormPostAuthenticate), useForm: true);
        });
    }
}