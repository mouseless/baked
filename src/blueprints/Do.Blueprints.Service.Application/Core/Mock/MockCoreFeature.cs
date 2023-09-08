using Do.Architecture;
using Microsoft.Extensions.Configuration;

namespace Do.Core.Mock;

public class MockCoreFeature : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<IConfiguration>(singleton: true);
            test.Mocks.Add<ISystem>(singleton: true);
        });
    }
}
