using Baked.Architecture;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new UriReturnIsRedirectConvention(), order: Order.At.Infra);
            conventions.Add(new UriReturnWithoutParameterIsGetConvention(), order: Order.At.Infra);
            conventions.Add(new UriReturnWithParameterIsFormPostConvention(), order: Order.At.Infra - 10);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.Add("System.Net");
        });
    }
}