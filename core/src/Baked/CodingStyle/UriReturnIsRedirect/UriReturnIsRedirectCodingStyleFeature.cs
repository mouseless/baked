using Baked.Architecture;
using Baked.CodingStyle.UriReturnIsRedirect;
using Baked.Domain.Configuration;

namespace Baked;

public static class UriReturnIsRedirectCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> UriReturnIsRedirect(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
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
    });
}