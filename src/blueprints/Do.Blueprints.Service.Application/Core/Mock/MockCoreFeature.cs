using Do.Architecture;

namespace Do.Core.Mock;

public class MockCoreFeature : ICoreFeature
{
    public string Id => GetType().Name;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<ISystem>(singleton: true);
        });
    }
}
