using Do.Architecture;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(MockClientBuilder _builder)
    : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(tests =>
        {
            foreach (var descriptor in _builder.Build())
            {
                tests.Mocks.Add(descriptor);
            }
        });
    }
}
