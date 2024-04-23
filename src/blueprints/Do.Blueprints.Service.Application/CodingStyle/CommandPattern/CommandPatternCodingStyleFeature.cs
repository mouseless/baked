using Do.Architecture;

namespace Do.CodingStyle.CommandPattern;

public class CommandPatternCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Insert(0, new RemoveTransientServicesWithNonPublicInitializerConvention());
            conventions.Insert(0, new InitializeUsingQueryParametersConvention());
        });
    }
}