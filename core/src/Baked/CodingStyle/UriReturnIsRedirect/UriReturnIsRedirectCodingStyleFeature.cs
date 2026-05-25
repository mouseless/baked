using Baked.Architecture;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.Add(new UriReturnIsRedirectConvention());
            conventions.Add(new UriReturnWithoutParameterIsGetConvention());
            conventions.Add(new UriReturnWithParameterIsFormPostConvention(), order: -10);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.Add("System.Net");
        });
    }
}