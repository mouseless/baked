using Do.Architecture;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new UriReturnIsRedirectConvention());
            conventions.Add(new UriReturnWithoutParameterIsGetConvention());
            conventions.Add(new UriReturnWithParameterIsFormPostConvention());
        });
    }
}