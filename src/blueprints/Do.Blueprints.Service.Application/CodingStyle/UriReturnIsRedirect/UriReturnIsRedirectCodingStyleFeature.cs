using Do.Architecture;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("System.Net");
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new UriReturnIsRedirectConvention());
            conventions.Add(new UriReturnWithoutParameterIsGetConvention());
            conventions.Insert(0, new UriReturnWithParameterIsFormPostConvention());
        });
    }
}