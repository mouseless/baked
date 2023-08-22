using Do.Architecture;

namespace Do.Core.Mock;

public class MockCoreFeature : IFeature, ICoreFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<ISystem>(singleton: true);
        });
    }
}
