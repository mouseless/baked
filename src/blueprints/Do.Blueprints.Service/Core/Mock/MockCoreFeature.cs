using Do.Architecture;

namespace Do.Core.Mock;

public class MockCoreFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTesting(testing =>
        {
            testing.Mocks.Add<ISystem>(singleton: true);
        });
    }
}
