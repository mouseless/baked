using Baked.Architecture;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new UriReturnIsRedirectConvention());
            builder.Conventions.Add(new UriReturnWithoutParameterIsGetConvention());
            builder.Conventions.Add(new UriReturnWithParameterIsFormPostConvention(), order: -10);
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("System.Net");
        });
    }
}